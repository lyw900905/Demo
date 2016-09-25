/********************************************************************************
** auth： lyw
** date： 2016-09-12
** desc： 客户端调用
** Ver.:  V1.0.0
*********************************************************************************/

using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace TestHttp
{
    using Common.Entity;
    using Common.Servers;

    /// <summary>
    /// 客户端调用服务类
    /// </summary>
    public class TestHttpClient
    {
        /// <summary>
        /// 静态实例字段
        /// </summary>
        private static TestHttpClient instance;

        /// <summary>
        /// 单例操作锁
        /// </summary>
        private static Object instanceLock = new object();

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private TestHttpClient()
        {

        }

        /// <summary>
        /// 静态实例属性
        /// </summary>
        public static TestHttpClient Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (instanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new TestHttpClient();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// 客户端监听
        /// </summary>
        private HttpListener clientListener;

        /// <summary>
        /// 异步调用方法
        /// </summary>
        private AsyncCallback ac = null;

        /// <summary>
        /// 请求监听地址
        /// </summary>
        private static String _strUrl = "http://127.0.0.1:2020/";

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
        /// 发送信息
        /// </summary>
        /// <param name="str">要发送的数据字符串</param>
        public void SendMsg(String str)
        {
            try
            {
                sendStr = str;
                httpWebRequest = (HttpWebRequest)WebRequest.Create(_strUrl);
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
        public void StartConnet()
        {
            // 查询服务器更新数据timer初始化及参数设置
            connectTimer = new System.Timers.Timer();
            connectTimer.Interval = 2 * 1000;
            connectTimer.AutoReset = false;
            connectTimer.Elapsed -= connectTimer_Elapsed;
            connectTimer.Elapsed +=connectTimer_Elapsed;
            connectTimer.Start();
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns>返回响应数据流Stream</returns>
        private Stream GetMsg()
        {
            httpWebRequest = (HttpWebRequest)WebRequest.Create(_strUrl);
            httpWebRequest.Method = "GET";
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream res = null;
            res = httpWebResponse.GetResponseStream();
            return res;
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
            ac = new AsyncCallback(GetContextAsynCallback);
            clientListener.BeginGetContext(ac, null);
            Console.WriteLine(DateTime.Now.ToString());
        }

        /// <summary>
        /// 停止客户端监听
        /// </summary>
        public void StopClientListener()
        {
            clientListener.Stop();
        }

        /// <summary>
        /// 订阅方法
        /// </summary>
        /// <param name="dealAction">委托方法</param>
        public void Subscriber(Action<List<UserInfo>> dealAction)
        {
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
            using (Stream stream = httpWebRequest.EndGetRequestStream(asy))
            {
                Byte[] sendMsg = Encoding.UTF8.GetBytes(sendStr);
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
                    response.OutputStream.Close();
                    output.Close();
                }
            }

            clientListener.BeginGetContext(ac, null);
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
                Stream res = GetMsg();
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
                connectTimer.Start();
            }
        }
    }
}
