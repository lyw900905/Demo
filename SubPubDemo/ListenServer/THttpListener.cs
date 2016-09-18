/********************************************************************************
** auth： lyw
** date： 2016-09-12
** desc： httpserver
** Ver.:  V1.0.0
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;


namespace ListenServer
{
    using Common.Entity;
    using Common.Helper;
    using Common.DAL;

    public class THttpListener
    {
        HttpListener listener;

        public THttpListener(string[] prefixes)
        {
            listener = new HttpListener();

            foreach (var item in prefixes)
            {
                listener.Prefixes.Add(item);
            }
        }

        public delegate void ResponseEventArges(HttpListenerContext ctx);
        //public event ResponseEventArges ResponseEvent;
        AsyncCallback ac = null;

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
                HttpListenerResponse response = ctx.Response;

                if (request.HttpMethod == "POST")
                {
                    Stream stream = request.InputStream;
                    string json = string.Empty;
                    StreamReader streamReader = new StreamReader(stream, request.ContentEncoding);
                    json = streamReader.ReadToEnd();
                    UserInfo userInfo = JSonHelper.Deserialize<UserInfo>(json);
                    UserInfoDAL.AddUserInfo(userInfo);
                }

                List<UserInfo> userList = UserInfoDAL.QueryAllUserInfo();

                // 收到连接请求回传
                string responseString = JSonHelper.Serialize(userList);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
                output.Close();
            }
            listener.BeginGetContext(ac, null);
        }
    }


}
