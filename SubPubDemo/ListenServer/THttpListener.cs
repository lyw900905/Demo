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

    public class THttpListener
    {
        HttpListener listener; //todo:不管是public还是private，都必须加上对应的修饰符

        public THttpListener(string[] prefixes) //todo:注意代码注释和变量命名
        {
            listener = new HttpListener();

            foreach (var item in prefixes)
            {
                listener.Prefixes.Add(item);
            }
        }

        public delegate void ResponseEventArges(HttpListenerContext ctx); //todo:注意代码注释
        //public event ResponseEventArges ResponseEvent;
        Encoding encoding = Encoding.UTF8; //todo:不管是public还是private，都必须加上对应的修饰符
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
                    StreamReader streamReader = new StreamReader(stream, encoding);
                    json = streamReader.ReadToEnd();
                    UserInfo userInfo = JSonHelper.Deserialize<UserInfo>(json);//todo:没有对数据的有效性进行验证
                    UserInfoDAL.AddUserInfo(userInfo); //todo:用户积分是一个变化的过程，不是说用户积分只会提交一次
                }

                //todo:没有按照需求处理，需求是：按照积分排序，只取指定数量的用户积分信息给前端
                List<UserInfo> userList = UserInfoDAL.QueryAllUserInfo(); //todo:每次都从数据库查询，如果用户量稍微有点多就会导致数据库压力过大

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
