﻿//***********************************************************************************
// 文件名称：AnalysisService.cs
// 功能描述：解析服务帮助类
// 数据表：userinfo
// 作者：Lyevn
// 日期：2016/9/12 20:10:20
// 修改记录：
//***********************************************************************************

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

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
        private const Encoding mEncoding = Encoding.UTF8;

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
                String json = String.Empty;

                // 读取json数据流
                using (StreamReader strReader = new StreamReader(stream, mEncoding))
                {
                    json = strReader.ReadToEnd();
                }

                // 反序列化操作
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
                
                // 读取json数据流
                using (StreamReader streamReader = new StreamReader(stream, mEncoding))
                {
                    json = streamReader.ReadToEnd();
                }

                // 反序列化
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
