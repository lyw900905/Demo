/********************************************************************************
** auth： lyw
** date： 2016-09-12
** desc： httpserver
** Ver.:  V1.0.0
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace ListenServer
{
    using Common.Entity;
    using Common.Helper;
    using Common.DAL;
    using Common.Servers;

    public class THttpListener
    {
        /// <summary>
        /// 监听
        /// </summary>
        private HttpListener listener;

        /// <summary>
        /// 异步调用方法
        /// </summary>
        private AsyncCallback ac = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="prefixes">监听路径字符串</param>
        public THttpListener(String[] prefixes)
        {
            listener = new HttpListener();

            foreach (var item in prefixes)
            {
                listener.Prefixes.Add(item);
            }
        }

        /// <summary>
        /// 开始监听服务
        /// </summary>
        public void Start()
        {
            if (!listener.IsListening)
            {
                listener.Start();
                ac = new AsyncCallback(GetContextAsynCallback);
                listener.BeginGetContext(ac, null);
                Console.WriteLine(DateTime.Now.ToString());
            }

        }

        /// <summary>
        /// 停止监听服务
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }

        /// <summary>
        /// 请求信息字典列表
        /// </summary>
        private Dictionary<IPEndPoint, HttpListenerContext> lstListener = new Dictionary<IPEndPoint, HttpListenerContext>();

        /// <summary>
        /// 收到监听请求回调
        /// </summary>
        /// <param name="ia">异步结束状态</param>
        public void GetContextAsynCallback(IAsyncResult ia)
        {
            if (ia.IsCompleted)
            {
                HttpListenerContext ctx = listener.EndGetContext(ia);

                ctx.Response.StatusCode = 200;
                HttpListenerRequest request = ctx.Request;

                //if (!lstListener.ContainsKey(ctx.Request.RemoteEndPoint))
                //{
                //    lstListener[ctx.Request.RemoteEndPoint] = ctx;
                //}

                HttpListenerResponse response = ctx.Response;

                if (request.HttpMethod == "POST")
                {
                    Stream stream = request.InputStream;

                    UserInfo userInfo = AnalysisService.AnalysisJsonStre(stream);
                    UserInfoDAL.AddUserInfo(userInfo);

                }

                List<UserInfo> userList = UserInfoDAL.QueryAllUserInfo();
                // 收到连接请求回传
                String responseString = JSonHelper.Serialize(userList);
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                Int32 cout = 0;

                response.ContentLength64 = buffer.Length; 
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
                output.Close();
                Console.WriteLine("传输次数：" + cout++);

                //SendToClient();
            }

            listener.BeginGetContext(ac, null);
        }

        /// <summary>
        /// 测试发送数据到客户端端
        /// </summary>
        public void SendToClient()
        {
            List<UserInfo> userList = UserInfoDAL.QueryAllUserInfo();
            // 收到连接请求回传
            String responseString = JSonHelper.Serialize(userList);
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            Int32 cout = 0;

            foreach (var lst in lstListener)
            {
                lst.Value.Response.ContentLength64 = buffer.Length;
                System.IO.Stream output = lst.Value.Response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                lst.Value.Response.OutputStream.Close();
                output.Close();
                Console.WriteLine("传输次数：" + cout++);
            }

        }
    }
}
