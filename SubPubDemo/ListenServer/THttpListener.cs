//***********************************************************************************
// 文件名称：THttpListener.cs
// 功能描述：服务器监听类
// 数据表：
// 作者：Lyevn
// 日期：2016/9/12 20:10:20
// 修改记录：
//***********************************************************************************

using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Configuration;
using System.Collections.Generic;

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
        private static THttpListener mInstance;

        /// <summary>
        /// 单例操作锁
        /// </summary>
        private static Object mInstanceLock = new Object();

        /// <summary>
        /// 静态实例属性
        /// </summary>
        public static THttpListener Instance
        {
            get
            {
                if (mInstance == null)
                {
                    lock (mInstanceLock)
                    {
                        if (mInstance == null)
                        {
                            mInstance = new THttpListener();
                        }
                    }
                }

                return mInstance;
            }
        }

        /// <summary>
        /// 监听
        /// </summary>
        private HttpListener mServerListener;

        /// <summary>
        /// 发送数据
        /// </summary>
        private String mSendStr = String.Empty;

        /// <summary>
        /// 请求监听地址
        /// </summary>
        private static String mStrUrl = "http://127.0.0.1:3030/";

        /// <summary>
        /// 请求
        /// </summary>
        private HttpWebRequest mHttpWebRequest;

        /// <summary>
        /// 异步调用方法
        /// </summary>
        private AsyncCallback mCallBack = null;

        /// <summary>
        /// 是否更新
        /// </summary>
        private Boolean mIsUpdate = false;

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private THttpListener()
        {

        }

        /// <summary>
        /// 开启监听服务线程
        /// </summary>
        public void StartServerThread()
        {
            Thread th = new Thread(new ThreadStart(StartServerListener));
            th.Start();
        }

        /// <summary>
        /// 开始监听服务
        /// </summary>
        public void StartServerListener()
        {
            try
            {
                mServerListener = new HttpListener();
                String serverStrUrl = ConfigurationManager.AppSettings["ServerListenerUrl"];
                mServerListener.Prefixes.Add(serverStrUrl);
                mServerListener.Start();
                mCallBack = new AsyncCallback(GetContextAsynCallback);
                mServerListener.BeginGetContext(mCallBack, null);

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
            if (mServerListener != null && mServerListener.IsListening)
            {
                mServerListener.Stop();
            }
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
                    HttpListenerContext ctx = mServerListener.EndGetContext(ia);
                    ctx.Response.StatusCode = 200;
                    HttpListenerRequest request = ctx.Request;
                    HttpListenerResponse response = ctx.Response;
                    String responseString = String.Empty;

                    // 请求类型判断
                    if (request.HttpMethod == "POST")
                    {
                        // 解析数据
                        Stream stream = request.InputStream;
                        UserInfo userInfo = AnalysisService.AnalysisJsonStre(stream);

                        // 数据判断
                        if (userInfo != null)
                        {
                            UserInfoDAL.AddUserInfo(userInfo);//// 更新数据库
                            mIsUpdate = true;
                        }
                    }

                    #region 通过客户端请求更新

                    if (request.HttpMethod == "GET" && mIsUpdate)
                    {
                        List<UserInfo> userList = UserInfoDAL.QueryAllUserInfo();

                        // 收到连接请求回传
                        responseString = JsonConvert.SerializeObject(userList);
                    }

                    // 将数据转换为byte[]
                    byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                    response.ContentLength64 = buffer.Length;

                    // 写入输出数据
                    using (Stream output = response.OutputStream)
                    {
                        output.Write(buffer, 0, buffer.Length);
                        output.Close();
                    }

                    #endregion

                    #region 互相监听时更新数据

                    // 由于客户端地址只有一个开启多个客户端使用时异常
                    //OnUpdate();

                    #endregion
                }

                mServerListener.BeginGetContext(mCallBack, null);
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

            SendRequestMsg(responseString);
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="str">String型：要发送的数据</param>
        private void SendRequestMsg(String str)
        {
            // 通过客户端开启监听，服务器发送数据到客户端监听地址
            try
            {
                mSendStr = str;
                mHttpWebRequest = (HttpWebRequest)WebRequest.Create(mStrUrl);
                mHttpWebRequest.Method = "POST";
                mHttpWebRequest.BeginGetRequestStream(new AsyncCallback(PostCallBack), mHttpWebRequest);
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
            mHttpWebRequest = (HttpWebRequest)asy.AsyncState;

            Byte[] sendMsg = Encoding.UTF8.GetBytes(mSendStr);//// 发送数据流

            // 写入向监听推送的数据
            using (Stream stream = mHttpWebRequest.EndGetRequestStream(asy))
            {
                stream.Write(sendMsg, 0, sendMsg.Length);
                stream.Close();
            }

            mHttpWebRequest.GetResponse();
        }
    }
}
