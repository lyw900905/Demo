/********************************************************************************
** auth： lyw
** date： 2016-09-21
** desc： 测试数据生成服务类
** Ver.:  V1.0.0
*********************************************************************************/
using System;

namespace Common.Servers
{
    using Common.Entity;
    using Newtonsoft.Json;

    /// <summary>
    /// 测试数据生成服务类
    /// </summary>
    public class TestDataService
    {
        /// <summary>
        /// 用户测试数据生成方法
        /// </summary>
        /// <returns>string类型</returns>
        public static String CreateTestData()
        {
            UserInfo userInfo = new UserInfo();
            Random rd = new Random();
            Int32 num = rd.Next(1, 150);
            String str = Guid.NewGuid().ToString("N");
            userInfo.UserName = "测试用户名" + str.Substring(0, 4);
            userInfo.UserIntegral = num;

            return JsonConvert.SerializeObject(userInfo);
        }
    }
}
