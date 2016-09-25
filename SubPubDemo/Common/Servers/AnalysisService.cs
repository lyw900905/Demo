﻿/********************************************************************************
** auth： lyw
** date： 2016-09-20
** desc： AnalysisService
** Ver.:  V1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Common.Servers
{
    using Common.Entity;
    using Common.Helper;
    using Newtonsoft.Json;

    /// <summary>
    /// 解析服务类
    /// </summary>
    public class AnalysisService
    {
        /// <summary>
        /// 数据编码
        /// </summary>
        private static Encoding encoding = Encoding.UTF8;

        /// <summary>
        /// 解析Json数据流,返回用户list
        /// </summary>
        /// <param name="stream">json数据流</param>
        /// <returns>用户列表,异常返回Null</returns>
        public static List<UserInfo> AnalysisJsonStream(Stream stream)
        {
            try
            {
                List<UserInfo> lstInfo = new List<UserInfo>();
                StreamReader strReader = new StreamReader(stream, encoding);
                String json = strReader.ReadToEnd();
                lstInfo = JsonConvert.DeserializeObject<List<UserInfo>>(json);
                return lstInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine("解析json数据流错误异常：" + ex);
                return null;
            }
        }

        /// <summary>
        /// 解析json数据流，返回用户信息
        /// </summary>
        /// <param name="stream">用户json数据流</param>
        /// <returns>用户信息</returns>
        public static UserInfo AnalysisJsonStre(Stream stream)
        {
            try
            {
                String json = String.Empty;
                StreamReader streamReader = new StreamReader(stream, encoding);
                json = streamReader.ReadToEnd();
                UserInfo userInfo = JsonConvert.DeserializeObject<UserInfo>(json);
                return userInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine("解析json数据流错误异常：" + ex);
                return null;
            }

        }
    }
}
