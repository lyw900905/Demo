//***********************************************************************************
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
    using Newtonsoft.Json;

    /// <summary>
    /// 解析服务类
    /// </summary>
    public class AnalysisService
    {
        /// <summary>
        /// 解析Json数据流,返回用户list
        /// </summary>
        /// <param name="stream">json数据流</param>
        /// <returns>用户信息列表</returns>
        public static List<UserInfo> AnalysisJsonStream(Stream stream, Encoding encoding)
        {
            List<UserInfo> userInfoList = new List<UserInfo>();
            String json = String.Empty;

            // 读取json数据流
            using (StreamReader strReader = new StreamReader(stream, encoding))
            {
                json = strReader.ReadToEnd();
            }

            // 反序列化操作
            userInfoList = JsonConvert.DeserializeObject<List<UserInfo>>(json);

            return userInfoList;
        }

        /// <summary>
        /// 解析json数据流，返回用户信息
        /// </summary>
        /// <param name="stream">用户json数据流</param>
        /// <param name="encoding">传输数据编码类型</param>
        /// <returns>用户信息</returns>
        public static UserInfo AnalysisJsonStre(Stream stream, Encoding encoding)
        {
            String json = String.Empty;

            // 读取json数据流
            using (StreamReader streamReader = new StreamReader(stream, encoding))
            {
                json = streamReader.ReadToEnd();
            }

            // 反序列化
            UserInfo userInfo = JsonConvert.DeserializeObject<UserInfo>(json);

            return userInfo;
        }
    }
}
