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
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    using DAL;
    using Model.Entities;
    using Newtonsoft.Json;

    /// <summary>
    /// 服务器调用服务类
    /// </summary>
    public class HttpListenerBLL
    {
        #region 静态字段

        /// <summary>
        /// 静态实例字段
        /// </summary>
        private static HttpListenerBLL mInstance;

        /// <summary>
        /// 单例操作锁
        /// </summary>
        private static Object mInstanceLock = new Object();

        /// <summary>
        /// 请求监听地址
        /// </summary>
        private static String mStrUrl = "http://127.0.0.1:3030/";

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
        private String sendStr = String.Empty;

        /// <summary>
        /// 异步调用方法
        /// </summary>
        private AsyncCallback callBack = null;

        #endregion

        #region 属性

        /// <summary>
        /// 静态实例属性
        /// </summary>
        public static HttpListenerBLL Instance
        {
            get
            {
                if (mInstance == null)
                {
                    lock (mInstanceLock)
                    {
                        if (mInstance == null)
                        {
                            mInstance = new HttpListenerBLL();
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
        private HttpListenerBLL()
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
            String serverStrUrl =String.Empty;//ConfigurationManager.AppSettings["ServerListenerUrl"];//// 配置文件中获取服务器监听url
            serverListener.Prefixes.Add(serverStrUrl);
            serverListener.Start();
            callBack = new AsyncCallback(GetContextAsynCallback);////设置回调函数
            serverListener.BeginGetContext(callBack, null);////开始异步传输

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

        private Action dealAction;

        private void DealAction()
        {
        }

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
            if (!asyncResult.IsCompleted)
            {
                serverListener.BeginGetContext(callBack, null);
            }

            // 使用异步线程处理回调信息
            Task.Factory.StartNew(new Action(delegate
            {
                HttpListenerContext ctx = serverListener.EndGetContext(asyncResult);
                ctx.Response.StatusCode = mStatusCode;

                HttpListenerRequest request =ctx.Request;

                HttpListener httpListener=new HttpListener();
                //HttpContext httpContext = new HttpContext(ActionLink("TEXT", "ACTION", "CONTROLLER"));
                HttpListenerContext httpCont = httpListener.GetContext();
                //HttpRequest httpRequest = httpContext.Request;

                // 请求类型判断
                if (request.HttpMethod == "POST")
                {
                    // 解析数据
                    Stream stream = request.InputStream;
                    UserInfo userInfo =new UserInfo(); //AnalysisService.AnalysisJsonStre(stream, mEncoding);

                    // 数据判断
                    if (userInfo != null)
                    {
                        UserInfoDAL.AddUserInfo(userInfo);//// 更新数据库
                    }
                }

                #region 通过客户端请求更新

                String responseString = String.Empty;
                if (request.HttpMethod == "GET")
                {
                    List<UserInfo> userList = UserInfoDAL.QueryAllUserInfo();

                    // 收到连接请求回传
                    responseString = JsonConvert.SerializeObject(userList);
                }

                // 将数据转换为byte[]
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);

                HttpListenerResponse response = ctx.Response;
                response.ContentLength64 = buffer.Length;

                // 写入输出数据
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

            }));
        }

        /// <summary>
        /// 获取返回请求的数据
        /// </summary>
        private void GetResponseData()
        {
            // 获取数据库中用户信息
            List<UserInfo> userList = UserInfoDAL.QueryAllUserInfo();
            String responseString = JsonConvert.SerializeObject(userList);

            // 发送返回请求的数据信息
            SendRequestMessage(responseString);
        }

        /// <summary>
        /// 发送请求信息
        /// </summary>
        /// <param name="responseStr">String型：要发送的Json字符串</param>
        private void SendRequestMessage(String responseStr)
        {
            // 通过客户端开启监听，服务器发送数据到客户端监听地址
            sendStr = responseStr;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(mStrUrl);
            httpWebRequest.Method = "POST";

            // 开始异步请求传输
            httpWebRequest.BeginGetRequestStream(new AsyncCallback(PostCallBack), httpWebRequest);
        }

        /// <summary>
        /// 发送数据回调函数
        /// </summary>
        /// <param name="asyncResult">异步结束状态</param>
        private void PostCallBack(IAsyncResult asyncResult)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)asyncResult.AsyncState;
            Byte[] sendMsg = Encoding.UTF8.GetBytes(sendStr);//// 发送数据流

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
