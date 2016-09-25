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
using System.Threading;
using System.Configuration;

namespace ListenServer
{
    using Common.Entity;
    using Common.DAL;
    using Common.Servers;
    using Newtonsoft.Json;

    /// <summary>
    /// 服务器调用服务类
    /// </summary>
    public class THttpListener
    {
        /// <summary>
        /// 静态实例字段
        /// </summary>
        private static THttpListener instance;

        /// <summary>
        /// 单例操作锁
        /// </summary>
        private static Object instanceLock = new object();

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private THttpListener()
        {

        }

        /// <summary>
        /// 静态实例属性
        /// </summary>
        public static THttpListener Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (instanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new THttpListener();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// 监听
        /// </summary>
        private HttpListener serverlistener;

        /// <summary>
        /// 发送数据
        /// </summary>
        private String sendStr = String.Empty;

        /// <summary>
        /// 请求监听地址
        /// </summary>
        private static String _strUrl = "http://127.0.0.1:3030/";

        /// <summary>
        /// 请求
        /// </summary>
        private HttpWebRequest httpWebRequest;

        /// <summary>
        /// 异步调用方法
        /// </summary>
        private AsyncCallback ac = null;

        /// <summary>
        /// 是否更新
        /// </summary>
        private Boolean isUpdate = false;

        /// <summary>
        /// 开始监听服务
        /// </summary>
        public void StartServerListener()
        {
            try
            {
                serverlistener = new HttpListener();
                String serverStrUrl = ConfigurationManager.AppSettings["ServerListenerUrl"];
                serverlistener.Prefixes.Add(serverStrUrl);
                serverlistener.Start();
                ac = new AsyncCallback(GetContextAsynCallback);
                serverlistener.BeginGetContext(ac, null);
                Console.WriteLine("开启服务器监听：" + DateTime.Now);
            }
            catch (Exception ex)
            {
                Console.WriteLine("开启服务器监听异常：" + ex);
            }
        }

        /// <summary>
        /// 停止监听服务
        /// </summary>
        public void StopServerListener()
        {
            serverlistener.Stop();
        }

        /// <summary>
        /// 收到监听请求回调
        /// </summary>
        /// <param name="ia">异步结束状态</param>
        private void GetContextAsynCallback(IAsyncResult ia)
        {
            // 收到客户端请求时回调函数
            // 异步操作完成
            // 推送操作：解析数据，添加到数据
            // 获取操作：从数据库获取数据并发送到客户端
            try
            {
                if (ia.IsCompleted)
                {
                    HttpListenerContext ctx = serverlistener.EndGetContext(ia);
                    ctx.Response.StatusCode = 200;
                    HttpListenerRequest request = ctx.Request;
                    HttpListenerResponse response = ctx.Response;

                    // 请求类型判断
                    if (request.HttpMethod == "POST")
                    {
                        Stream stream = request.InputStream;
                        UserInfo userInfo = AnalysisService.AnalysisJsonStre(stream);

                        // 数据判断
                        if (userInfo != null)
                        {
                            UserInfoDAL.AddUserInfo(userInfo);
                            isUpdate = true;
                        }
                    }

                    String responseString = String.Empty;

                    #region 通过客户端请求更新

                    if (request.HttpMethod == "GET" && isUpdate)
                    {
                        List<UserInfo> userList = UserInfoDAL.QueryAllUserInfo();

                        // 收到连接请求回传
                        responseString = JsonConvert.SerializeObject(userList);
                    }

                    // 将数据转换为byte[]
                    byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                    response.ContentLength64 = buffer.Length;

                    using (Stream output = response.OutputStream)
                    {
                        output.Write(buffer, 0, buffer.Length);
                        response.OutputStream.Close();
                        output.Close();
                    }

                    #endregion

                    #region 互相监听时更新数据

                    // 由于客户端地址只有一个开启多个客户端使用时异常
                    //OnUpdate();

                    #endregion
                }

                serverlistener.BeginGetContext(ac, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("收到请求，异步回调函数异常：" + ex);
            }

        }

        /// <summary>
        /// 测试发送数据到客户端
        /// </summary>
        private void OnUpdate()
        {
            // 客户端互相监听时，更新客户端数据
            List<UserInfo> userList = UserInfoDAL.QueryAllUserInfo();
            String responseString = JsonConvert.SerializeObject(userList);
            SendMsg(responseString);
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="str">String型：要发送的数据</param>
        private void SendMsg(String str)
        {
            // 发送数据到客户端监听地址
            try
            {
                sendStr = str;
                httpWebRequest = (HttpWebRequest)WebRequest.Create(_strUrl);
                httpWebRequest.Method = "POST";
                httpWebRequest.BeginGetRequestStream(new AsyncCallback(PostCallBack), httpWebRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine("向客户端监听发送数据异常：" + ex);
            }
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
                byte[] sendMsg = Encoding.UTF8.GetBytes(sendStr);
                stream.Write(sendMsg, 0, sendMsg.Length);
                stream.Close();
            }

            httpWebRequest.GetResponse();
        }
    }
}
