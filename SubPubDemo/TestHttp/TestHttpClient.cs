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

    class TestHttpClient //todo:注意规范。。class必须要有访问修饰符，class必须要有summary头
    {
        /// <summary>
        /// 静态实例字段
        /// </summary>
        private static TestHttpClient instance;

        /// <summary>
        /// 单例操作锁
        /// </summary>
        private static Object _lock = new object();

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
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new TestHttpClient();
                        }
                    }
                }//todo:注意按照规范写代码
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

        private static AutoResetEvent m_object = new AutoResetEvent(false);//todo:

        private System.Timers.Timer connectTimer;//todo:

        /// <summary>
        /// 发送数据
        /// </summary>
        private String sendStr = String.Empty;

        /// <summary>
        /// 请求
        /// </summary>
        private HttpWebRequest httpRequ;//todo:不要随便写缩写，常见的可以缩写，但是像这种本来就没缩写的，不要写缩写

        /// <summary>
        /// 应答
        /// </summary>
        private HttpWebResponse httpResp;//todo:不要随便写缩写，常见的可以缩写，但是像这种本来就没缩写的，不要写缩写

        /// <summary>
        /// 定义事件
        /// </summary>
        private event Action<List<UserInfo>> publishEvent;

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="str"></param>//todo:
        public void SendMsg(String str)//todo:
        {
            try
            {
                sendStr = str;
                httpRequ = (HttpWebRequest)WebRequest.Create(_strUrl);
                httpRequ.Method = "POST";
                httpRequ.BeginGetRequestStream(new AsyncCallback(PostCallBack), httpRequ);
                m_object.WaitOne();
                httpResp = (HttpWebResponse)httpRequ.GetResponse();
                httpResp.GetResponseStream();
                //return myStream;
            }
            catch (Exception ex)
            {
                Console.WriteLine("发送数据异常:" + ex);
            }
        }

        /// <summary>
        /// 开始连接查询
        /// </summary>
        public void StartConnet()//todo:
        {
            connectTimer = new System.Timers.Timer();
            connectTimer.Interval = 2 * 1000;
            connectTimer.AutoReset = false;
            connectTimer.Elapsed += new System.Timers.ElapsedEventHandler(connectTimer_Elapsed);
            connectTimer.Start();
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        private Stream GetMsg()
        {
            httpRequ = (HttpWebRequest)WebRequest.Create(_strUrl);
            httpRequ.Method = "GET";
            //httpRequ.BeginGetResponse(new AsyncCallback(ResponseCallBack), httpRequ);
            httpResp = (HttpWebResponse)httpRequ.GetResponse();
            Stream res = null;
            res = httpResp.GetResponseStream();
            return res;
        }

        /// <summary>
        /// 开启客户端监听
        /// </summary>
        public void StartClientListener()
        {
            clientListener = new HttpListener();
            clientListener.Prefixes.Add(clientStrUrl);

            if (!clientListener.IsListening)
            {
                clientListener.Start();
                ac = new AsyncCallback(GetContextAsynCallback);
                clientListener.BeginGetContext(ac, null);
                Console.WriteLine(DateTime.Now.ToString());
            }
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
        /// <param name="asy"></param>
        private void PostCallBack(IAsyncResult asy)
        {
            HttpWebRequest objReq = (HttpWebRequest)asy.AsyncState;
            Stream obj_Stream = objReq.EndGetRequestStream(asy);
            Byte[] send_Msg_Arr = Encoding.UTF8.GetBytes(sendStr);
            obj_Stream.Write(send_Msg_Arr, 0, send_Msg_Arr.Length);
            m_object.Set();
            obj_Stream.Close();
        }

        //private void ResponseCallBack(IAsyncResult asy)
        //{
        //    HttpWebRequest objReq = (HttpWebRequest)asy.AsyncState;
        //    httpResp = (HttpWebResponse)objReq.EndGetResponse(asy);
        //    Stream res = null;
        //    res = httpResp.GetResponseStream();
        //    Byte[] sb = Encoding.UTF8.GetBytes(clientStrUrl);
        //    res.Write(sb, 0, sb.Length);

        //    m_object.Set();
        //    res.Close();
        //}

        /// <summary>
        /// 收到监听请求回调
        /// </summary>
        /// <param name="ia">异步结束状态</param>
        private void GetContextAsynCallback(IAsyncResult ia)
        {
            if (ia.IsCompleted)
            {
                HttpListenerContext ctx = clientListener.EndGetContext(ia);

                ctx.Response.StatusCode = 200;
                HttpListenerRequest request = ctx.Request;
                HttpListenerResponse response = ctx.Response;
                if (request.HttpMethod == "POST")
                {
                    Stream stream = request.InputStream;
                    List<UserInfo> userInfo = AnalysisService.AnalysisJsonStream(stream);
                    OnUpdate(userInfo);
                }//todo:
                String res = "Ok";
                Byte[] buffer = Encoding.UTF8.GetBytes(res);
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();//todo:
                output.Close();//todo:
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
        /// <param name="sender"></param>//todo:
        /// <param name="e"></param>
        private void connectTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Stream res = GetMsg();
                List<UserInfo> lstuser = AnalysisService.AnalysisJsonStream(res);
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
