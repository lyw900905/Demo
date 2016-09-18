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

namespace TestHttp
{
    class TestHttpClient
    {
        private static string _strUrl = "http://127.0.0.1/";
        private static AutoResetEvent m_object = new AutoResetEvent(false);
        static string sendStr = string.Empty;
        static HttpWebRequest httpRequ = (HttpWebRequest)WebRequest.Create(_strUrl);
        static HttpWebResponse httpResp;
        //static Encoding encoding = Encoding.UTF8;

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="str"></param>
        public static Stream SendMsg(string str)
        {
            sendStr = str;
            httpRequ = (HttpWebRequest)WebRequest.Create(_strUrl);
            httpRequ.Method = "POST";
            httpRequ.BeginGetRequestStream(new AsyncCallback(PostCallBack), httpRequ);
            m_object.WaitOne();
            httpResp = (HttpWebResponse)httpRequ.GetResponse();
            Stream myStream = httpResp.GetResponseStream();
            return myStream;
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        public static Stream GetMsg()
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
        /// 发送数据回调函数
        /// </summary>
        /// <param name="asy"></param>
        private static void PostCallBack(IAsyncResult asy)
        {
            HttpWebRequest objReq = (HttpWebRequest)asy.AsyncState;
            Stream obj_Stream = objReq.EndGetRequestStream(asy);
            byte[] send_Msg_Arr = Encoding.UTF8.GetBytes(sendStr);
            obj_Stream.Write(send_Msg_Arr, 0, send_Msg_Arr.Length);
            m_object.Set();
            obj_Stream.Close();
        }

        /// <summary>
        /// 接收数据回调函数(未使用)
        /// </summary>
        /// <param name="asy"></param>
        private static void ResponseCallBack(IAsyncResult asy)
        {
            HttpWebRequest objReq = (HttpWebRequest)asy.AsyncState;
            httpResp = (HttpWebResponse)objReq.EndGetResponse(asy);
            Stream res = null;
            res = httpResp.GetResponseStream();
        }
    }
}
