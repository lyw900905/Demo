/********************************************************************************
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
        /// <returns>用户列表</returns>
        public static List<UserInfo> AnalysisJsonStream(Stream stream)
        {
            List<UserInfo> lstInfo = new List<UserInfo>();
            StreamReader strReader = new StreamReader(stream, encoding);
            String json = strReader.ReadToEnd();
            lstInfo = JSonHelper.Deserialize<List<UserInfo>>(json);
            return lstInfo;
        }

        /// <summary>
        /// 解析json数据流，返回用户信息
        /// </summary>
        /// <param name="stream">用户json数据流</param>
        /// <returns>用户信息</returns>
        public static UserInfo AnalysisJsonStre(Stream stream)
        {
            String json = String.Empty;
            StreamReader streamReader = new StreamReader(stream, encoding);
            json = streamReader.ReadToEnd();
            UserInfo userInfo = JSonHelper.Deserialize<UserInfo>(json);

            return userInfo;
        }
    }
}
