﻿/********************************************************************************
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

    public class THttpListener
    {
        /// <summary>
        /// 静态实例字段
        /// </summary>
        private static THttpListener instance;

        /// <summary>
        /// 单例操作锁
        /// </summary>
        private static Object _lock = new object(); //todo:不要随意命名变量，变量名称要能够体现出含义，不要随意加下划线之类的

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
                    lock (_lock)
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
        private HttpWebRequest httpRequ;

        private AutoResetEvent m_object = new AutoResetEvent(false); //todo:注意各种规范！！！

        /// <summary>
        /// 异步调用方法
        /// </summary>
        private AsyncCallback ac = null;

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

                if (!serverlistener.IsListening) //todo:就在前面new了一个对象，还添加了serverStrUrl。这儿判断是否处于监听还有用么？
                {
                    serverlistener.Start();
                    ac = new AsyncCallback(GetContextAsynCallback);
                    serverlistener.BeginGetContext(ac, null);
                    Console.WriteLine("开启服务器监听：" + DateTime.Now);
                }
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

        private Boolean isUpdate = false; //todo:注意代码格式，缺少summary头，和下面函数之间少一个空行，字段定义和属性定义应该各在一个代码段里，不要随意放置字段位置
        /// <summary>
        /// 收到监听请求回调
        /// </summary>
        /// <param name="ia">异步结束状态</param>
        private void GetContextAsynCallback(IAsyncResult ia) //todo:代码实现缺少必要注释
        {
            if (ia.IsCompleted)
            {
                HttpListenerContext ctx = serverlistener.EndGetContext(ia);

                ctx.Response.StatusCode = 200;
                HttpListenerRequest request = ctx.Request;

                HttpListenerResponse response = ctx.Response;

                if (request.HttpMethod == "POST")
                {
                    Stream stream = request.InputStream;
                    UserInfo userInfo = AnalysisService.AnalysisJsonStre(stream); //todo:如果stream中没有数据。你这儿不就异常了？
                    UserInfoDAL.AddUserInfo(userInfo);
                    isUpdate = true;
                }

                String responseString = String.Empty;
                #region 通过客户端请求更新
                if (request.HttpMethod == "GET" && isUpdate) //todo:region和实现代码之间需要一个空行
                {
                    //isUpdate = false;
                    List<UserInfo> userList = UserInfoDAL.QueryAllUserInfo();
                    // 收到连接请求回传
                    responseString = JsonConvert.SerializeObject(userList);
                }
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close(); //todo: 注意资源操作时，要保证必然能够正常释放和关闭资源。你这儿直接这么写是要出问题的
                output.Close();

                #endregion

                #region 互相监听
                // 由于客户端地址只有一个开启多个客户端使用时异常
                //OnUpdate();
                #endregion
            }

            serverlistener.BeginGetContext(ac, null);
        }

        /// <summary>
        /// 测试发送数据到客户端端
        /// </summary>
        private void OnUpdate()
        {
            List<UserInfo> userList = UserInfoDAL.QueryAllUserInfo();
            String responseString = JsonConvert.SerializeObject(userList);
            SendMsg(responseString);
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="str"></param>
        private void SendMsg(String str) //todo:注意代码注释
        {
            try
            {
                sendStr = str;
                httpRequ = (HttpWebRequest)WebRequest.Create(_strUrl);
                httpRequ.Method = "POST";
                httpRequ.BeginGetRequestStream(new AsyncCallback(PostCallBack), httpRequ); //todo:你这儿用异步，下面又等待，那你干嘛用异步？
                m_object.WaitOne();
                httpRequ.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine("向客户端监听发送数据异常：" + ex);
            }

        }

        /// <summary>
        /// 发送数据回调函数
        /// </summary>
        /// <param name="asy"></param>
        private void PostCallBack(IAsyncResult asy)
        {
            HttpWebRequest objReq = (HttpWebRequest)asy.AsyncState;
            Stream obj_Stream = objReq.EndGetRequestStream(asy);
            byte[] send_Msg_Arr = Encoding.UTF8.GetBytes(sendStr);
            obj_Stream.Write(send_Msg_Arr, 0, send_Msg_Arr.Length);
            m_object.Set();
            obj_Stream.Close();
        }
    }
}
