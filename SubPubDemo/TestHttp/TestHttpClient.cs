//***********************************************************************************
// 文件名称：TestHttpClient.cs
// 功能描述：请求服务器监听测试类
// 数据表：
// 作者：Lyevn
// 日期：2016/9/12 20:10:20
// 修改记录：
//***********************************************************************************

using System;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace TestHttp
{
    using Common.Entity;
    using Common.Servers;

    /// <summary>
    /// 客户端调用服务监听测试类
    /// </summary>
    public class TestHttpClient
    {
        /// <summary>
        /// 静态实例字段
        /// </summary>
        private static TestHttpClient mInstance;

        /// <summary>
        /// 单例操作锁
        /// </summary>
        private static Object mInstanceLock = new Object();

        /// <summary>
        /// 静态实例属性
        /// </summary>
        public static TestHttpClient Instance
        {
            get
            {
                if (mInstance == null)
                {
                    lock (mInstanceLock)
                    {
                        if (mInstance == null)
                        {
                            mInstance = new TestHttpClient();
                        }
                    }
                }

                return mInstance;
            }
        }

        /// <summary>
        /// 客户端监听
        /// </summary>
        private HttpListener clientListener;

        /// <summary>
        /// 异步调用方法
        /// </summary>
        private AsyncCallback callBack = null;

        /// <summary>
        /// 请求监听地址
        /// </summary>
        private static String mRequestServerStrUrl = "http://10.254.0.150:9005/";

        /// <summary>
        /// 客户端监听地址
        /// </summary>
        private String clientStrUrl = "http://127.0.0.1:3030/";

        /// <summary>
        /// 连接服务器更新timer
        /// </summary>
        private System.Timers.Timer connectTimer;

        /// <summary>
        /// 发送数据
        /// </summary>
        private String sendStr = String.Empty;

        /// <summary>
        /// 请求
        /// </summary>
        private HttpWebRequest httpWebRequest;

        /// <summary>
        /// 应答
        /// </summary>
        private HttpWebResponse httpWebResponse;

        /// <summary>
        /// 定义事件
        /// </summary>
        private event Action<List<UserInfo>> publishEvent;

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private TestHttpClient()
        {

        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="str">要发送的数据字符串</param>
        public void SendRequestMsg(String str)
        {
            // 发送请求的字符串到服务器监听
            try
            {
                sendStr = str;
                httpWebRequest = (HttpWebRequest)WebRequest.Create(mRequestServerStrUrl);
                httpWebRequest.Method = "POST";
                httpWebRequest.BeginGetRequestStream(new AsyncCallback(PostCallBack), httpWebRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine("发送数据异常:" + ex);
            }
        }

        /// <summary>
        /// 开始连接查询timer
        /// </summary>
        public void StartConnetQuery()
        {
            // 查询服务器更新数据timer初始化及参数设置
            connectTimer = new System.Timers.Timer();

            connectTimer.Interval = 2 * 1000;
            connectTimer.AutoReset = false;
            connectTimer.Elapsed -= connectTimer_Elapsed;
            connectTimer.Elapsed += connectTimer_Elapsed;

            connectTimer.Start();
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns>返回响应数据流Stream</returns>
        private Stream GetResponseMsg()
        {
            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(mRequestServerStrUrl);
                httpWebRequest.Method = "GET";
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                return httpWebResponse.GetResponseStream();
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取监听请求数据异常：" + ex);
                return null;
            }
        }

        /// <summary>
        /// 开启客户端监听
        /// </summary>
        public void StartClientListener()
        {
            // 通过开启客户端互相监听来更新客户端数据
            clientListener = new HttpListener();
            clientListener.Prefixes.Add(clientStrUrl);
            clientListener.Start();
            callBack = new AsyncCallback(GetContextAsynCallback);
            clientListener.BeginGetContext(callBack, null);

            Console.WriteLine("开启客户端监听：" + DateTime.Now.ToString());
        }

        /// <summary>
        /// 停止客户端监听
        /// </summary>
        public void StopClientListener()
        {
            if (clientListener != null && clientListener.IsListening)
            {
                clientListener.Stop();
            }
        }

        /// <summary>
        /// 订阅方法
        /// </summary>
        /// <param name="dealAction">委托方法</param>
        public void Subscriber(Action<List<UserInfo>> dealAction)
        {
            // 判断是否已经订阅此事件
            if (publishEvent != null && publishEvent.GetInvocationList().Length > 0)
            {
                foreach (var item in publishEvent.GetInvocationList())
                {
                    if (item.Equals(dealAction))
                    {
                        return;
                    }
                }
            }

            publishEvent += dealAction;
        }

        /// <summary>
        /// 发送数据回调函数
        /// </summary>
        /// <param name="asy">异步结束状态</param>
        private void PostCallBack(IAsyncResult asy)
        {
            httpWebRequest = (HttpWebRequest)asy.AsyncState;
            Byte[] sendMsg = Encoding.UTF8.GetBytes(sendStr);

            // 写入推送数据流
            using (Stream stream = httpWebRequest.EndGetRequestStream(asy))
            {
                stream.Write(sendMsg, 0, sendMsg.Length);
                stream.Close();
            }

            httpWebRequest.GetResponse();
        }

        /// <summary>
        /// 收到监听请求回调
        /// </summary>
        /// <param name="ia">异步结束状态</param>
        private void GetContextAsynCallback(IAsyncResult ia)
        {
            // 收到服务器请求时回调处理
            // 如果是推送数据就更新客户端界面显示
            if (ia.IsCompleted)
            {
                HttpListenerContext ctx = clientListener.EndGetContext(ia);
                ctx.Response.StatusCode = 200;
                HttpListenerRequest request = ctx.Request;
                HttpListenerResponse response = ctx.Response;

                // 判断推送数据
                if (request.HttpMethod == "POST")
                {
                    Stream stream = request.InputStream;
                    List<UserInfo> userInfo = AnalysisService.AnalysisJsonStream(stream);

                    // 数据判断
                    if (userInfo != null && userInfo.Count > 0)
                    {
                        OnUpdate(userInfo);
                    }
                }

                String res = "Ok";
                Byte[] buffer = Encoding.UTF8.GetBytes(res);
                response.ContentLength64 = buffer.Length;

                using (Stream output = response.OutputStream)
                {
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();
                }
            }

            clientListener.BeginGetContext(callBack, null);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="lstInfo">用户信息列表</param>
        private void OnUpdate(List<UserInfo> lstInfo)
        {
            if (publishEvent != null)
            {
                publishEvent(lstInfo);
            }
        }

        /// <summary>
        /// 定时查询服务器监听timer
        /// </summary>
        /// <param name="sender">timer对象</param>
        /// <param name="e">事件参数信息</param>
        private void connectTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // 每2秒从服务器获取更新数据并推送至界面显示
            try
            {
                Stream res = GetResponseMsg();
                List<UserInfo> lstuser = AnalysisService.AnalysisJsonStream(res);

                // 判断解析后数据是否为空
                if (lstuser != null && lstuser.Count > 0)
                {
                    OnUpdate(lstuser);

                    Console.WriteLine("更新数据时间：" + DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("异常信息：" + ex);
            }
            finally
            {
                // 重新开启查询
                connectTimer.Start();
            }
        }
    }
}
