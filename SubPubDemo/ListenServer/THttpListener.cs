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
using System.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        #region 静态字段

        /// <summary>
        /// 静态实例字段
        /// </summary>
        private static THttpListener mInstance;

        /// <summary>
        /// 单例操作锁
        /// </summary>
        private static Object mInstanceLock = new Object();

        /// <summary>
        /// 请求返回状态码
        /// </summary>
        private const Int32 mStatusCode = 200;

        /// <summary>
        /// 传输数据编码
        /// </summary>
        private static Encoding mEncoding = Encoding.UTF8;

        #endregion

        #region 字段

        /// <summary>
        /// 监听
        /// </summary>
        private HttpListener serverListener;

        /// <summary>
        /// 发送数据
        /// </summary>
        private String sendData = String.Empty;

        /// <summary>
        /// 异步调用方法
        /// </summary>
        private AsyncCallback callBack = null;

        #endregion

        #region 属性

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

        #endregion

        #region 构造函数

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private THttpListener()
        {

        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 开始启动服务器监听
        /// </summary>
        public void StartServerListener()
        {
            serverListener = new HttpListener();

            // 配置文件中获取服务器监听url并设置监听地址
            String serverStrUrl = ConfigurationManager.AppSettings["ServerListenerUrl"];
            serverListener.Prefixes.Add(serverStrUrl);

            //设置回调函数
            callBack = new AsyncCallback(GetContextAsynCallback);

            serverListener.Start();

            //开始异步传输
            serverListener.BeginGetContext(callBack, null);

            Console.WriteLine("开启服务器监听：" + DateTime.Now);
        }

        /// <summary>
        /// 停止服务器监听服务
        /// </summary>
        public void StopServerListener()
        {
            if (serverListener != null && serverListener.IsListening)
            {
                serverListener.Stop();
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 收到监听内容回调处理
        /// </summary>
        /// <param name="asyncResult">异步结束状态</param>
        private void GetContextAsynCallback(IAsyncResult asyncResult)
        {
            // 收到客户端请求时回调函数
            // 异步操作完成
            // 推送操作：解析数据，添加到数据
            // 获取操作：从数据库获取数据并发送到客户端
            if (asyncResult.IsCompleted)
            {
                // 使用异步线程处理回调信息
                Task.Factory.StartNew(new Action(delegate
                {
                    try
                    {
                        // 获取HttpListener监听请求内容
                        HttpListenerContext httpListenerContext = serverListener.EndGetContext(asyncResult);
                        httpListenerContext.Response.StatusCode = mStatusCode;
                        HttpListenerRequest request = httpListenerContext.Request;

                        // 客户端请求类型判断
                        if (request.HttpMethod == "POST")
                        {
                            // 解析请求的json数据流,返回用户信息实体
                            Stream stream = request.InputStream;
                            UserInfo userInfo = AnalysisService.AnalysisJsonStre(stream, mEncoding);

                            // 用户数据判断是否为空
                            if (userInfo != null)
                            {
                                UserInfoDAL.AddUserInfo(userInfo);//// 更新用户信息到数据库
                            }
                        }

                        #region 通过客户端请求更新

                        String responseString = String.Empty;
                        if (request.HttpMethod == "GET")
                        {
                            List<UserInfo> userInfoList = UserInfoDAL.QueryAllUserInfo();

                            // 收到连接请求回传
                            responseString = JsonConvert.SerializeObject(userInfoList);
                        }

                        // 将数据转换为byte[]
                        byte[] buffer = Encoding.UTF8.GetBytes(responseString);

                        HttpListenerResponse response = httpListenerContext.Response;
                        response.ContentLength64 = buffer.Length;

                        // 写入输出json数据流
                        using (Stream outputStream = response.OutputStream)
                        {
                            outputStream.Write(buffer, 0, buffer.Length);
                            outputStream.Close();
                        }

                        #endregion

                        #region 互相监听时更新数据

                        // 由于客户端地址只有一个开启多个客户端使用时异常
                        //GetResponseData();

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("处理回调信息时异常：" + ex);
                    }
                }));
            }

            // 再次开启异步接收
            serverListener.BeginGetContext(callBack, null);
        }

        /// <summary>
        /// 发送数据回调函数
        /// </summary>
        /// <param name="asyncResult">异步结束状态</param>
        private void PostCallBack(IAsyncResult asyncResult)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)asyncResult.AsyncState;
            Byte[] sendMsg = Encoding.UTF8.GetBytes(sendData);//// 发送数据流

            // 写入推送的数据
            using (Stream requestStream = httpWebRequest.EndGetRequestStream(asyncResult))
            {
                requestStream.Write(sendMsg, 0, sendMsg.Length);
                requestStream.Close();
            }

            httpWebRequest.GetResponse();
        }

        #endregion
    }
}
