/********************************************************************************
** auth： lyw
** date： 2016-09-12
** desc： 客户端调用
** Ver.:  V1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace TestHttp
{
    class TestHttpClient
    {
        private static string _strUrl = "http://127.0.0.1/";

        static string sendStr = string.Empty;
        static HttpWebRequest httpRequ = (HttpWebRequest)WebRequest.Create(_strUrl);
        static HttpWebResponse httpResp;

        /// <summary>
        /// 连接http服务
        /// </summary>
        /// <returns></returns>
        public static void ConnectHttpServer()
        {
            httpResp = (HttpWebResponse)httpRequ.GetResponse();
            Stream res = httpResp.GetResponseStream();
        }

        public static void SendMsg(string str)
        {
            sendStr = str;

            httpRequ.Method = "POST";
            httpRequ.BeginGetRequestStream(new AsyncCallback(PostCallBack), httpRequ);

            HttpWebResponse myHttpRes = (HttpWebResponse)httpRequ.GetResponse();
            Stream myStream = myHttpRes.GetResponseStream();


        }

        private static void PostCallBack(IAsyncResult asy)
        {
            HttpWebRequest objReq = (HttpWebRequest)asy.AsyncState;
            Stream obj_Stream = objReq.EndGetRequestStream(asy);

            byte[] send_Msg_Arr = Encoding.UTF8.GetBytes(sendStr);

            obj_Stream.Write(send_Msg_Arr, 0, send_Msg_Arr.Length);
            obj_Stream.Close();
        }
    }
}
