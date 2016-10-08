//***********************************************************************************
// 文件名称：TestHttpClient.cs
// 功能描述：请求服务器监听测试类
// 数据表：
// 作者：Lyevn
// 日期：2016/9/12 20:10:20
// 修改记录：
//***********************************************************************************

using System;
using System.Text;
using System.Net;
using System.IO;
using System.Timers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Timer = System.Timers.Timer;

namespace TestHttp
{
    using Common.Entity;
    using Common.Servers;

    /// <summary>
    /// 客户端调用服务监听测试类
    /// </summary>
    public class HttpClientTest
    {
        #region 静态字段

        /// <summary>
        /// 静态实例字段
        /// </summary>
        private static HttpClientTest mInstance;

        /// <summary>
        /// 单例操作锁
        /// </summary>
        private static Object mInstanceLock = new Object();

        /// <summary>
        /// 传输数据编码
        /// </summary>
        private static Encoding mEncoding = Encoding.UTF8;

        /// <summary>
        /// 请求监听地址
        /// </summary>
        private static String mRequestServerUrl = "http://10.254.0.150:9005/";

        /// <summary>
        /// 请求数据监听返回状态码
        /// </summary>
        private const Int32 mStatusCode = 200;

        #endregion

        #region 字段

        /// <summary>
        /// 连接服务器更新timer
        /// </summary>
        private Timer connectTimer;

        /// <summary>
        /// 发送数据
        /// </summary>
        private String sendData = String.Empty;

        /// <summary>
        /// 更新线程
        /// </summary>
        private Thread qureyThread = null;

        #endregion

        #region 事件

        /// <summary>
        /// 定义界面更新事件
        /// </summary>
        public event Action<List<UserInfo>> PublishEvent;

        #endregion

        #region 属性

        /// <summary>
        /// 静态实例属性
        /// </summary>
        public static HttpClientTest Instance
        {
            get
            {
                if (mInstance == null)
                {
                    lock (mInstanceLock)
                    {
                        if (mInstance == null)
                        {
                            mInstance = new HttpClientTest();
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
        private HttpClientTest()
        {

        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="sendJsonString">要发送的数据字符串</param>
        public void SendRequestMsg(String sendJsonString)
        {
            //// 发送请求的字符串到服务器监听
            Task.Factory.StartNew(new Action(delegate
            {
                try
                {
                    sendData = sendJsonString;
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(mRequestServerUrl);
                    httpWebRequest.Method = "POST";
                    httpWebRequest.BeginGetRequestStream(new AsyncCallback(PostCallBack), httpWebRequest);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("发送数据异常:" + ex);
                }
            }));
        }

        /// <summary>
        /// 开始连接查询timer
        /// </summary>
        public void StartConnetQueryTimer()
        {
            // 查询服务器更新数据timer初始化及参数设置
            connectTimer = new Timer();

            connectTimer.Interval = 2 * 1000;
            connectTimer.AutoReset = false;
            connectTimer.Elapsed += connectTimer_Elapsed;

            connectTimer.Start();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns>返回响应数据流Stream</returns>
        private Stream GetResponseMessage()
        {
            // 创建监听，设置参数信息
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(mRequestServerUrl);
            httpWebRequest.Method = "GET";

            // 获取请求返回
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            // 返回请求获取到的数据流
            return httpWebResponse.GetResponseStream();
        }

        /// <summary>
        /// 发送数据回调函数
        /// </summary>
        /// <param name="asyncResult">异步结束状态</param>
        private void PostCallBack(IAsyncResult asyncResult)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)asyncResult.AsyncState;
            Byte[] sendMsg = Encoding.UTF8.GetBytes(sendData);

            // 写入推送数据流
            using (Stream stream = httpWebRequest.EndGetRequestStream(asyncResult))
            {
                stream.Write(sendMsg, 0, sendMsg.Length);
                stream.Close();
            }

            httpWebRequest.GetResponse();
        }

        /// <summary>
        /// 发布信息
        /// </summary>
        /// <param name="userInfoList">用户信息列表</param>
        private void Publish(List<UserInfo> userInfoList)
        {
            if (PublishEvent != null)
            {
                PublishEvent(userInfoList);
            }
        }

        /// <summary>
        /// 定时查询服务器监听timer
        /// </summary>
        /// <param name="sender">timer对象</param>
        /// <param name="e">事件参数信息</param>
        private void connectTimer_Elapsed(Object sender, ElapsedEventArgs e)
        {
            // 每2秒从服务器获取更新数据并推送至界面显示
            try
            {
                Stream resultStream = GetResponseMessage();
                List<UserInfo> userInfoList = AnalysisService.AnalysisJsonStream(resultStream, mEncoding);

                // 判断解析后数据是否为空
                if (userInfoList != null && userInfoList.Count > 0)
                {
                    Publish(userInfoList);

                    Console.WriteLine("更新数据时间：" + DateTime.Now);
                }
                else
                {
                    Console.WriteLine("解析数据为空");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("异常信息：" + ex);
            }
            finally
            {
                // 重新开启查询timer
                connectTimer.Start();
            }
        }

        #endregion
    }
}
