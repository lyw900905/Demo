//***********************************************************************************
// 文件名称：TestDataService.cs
// 功能描述：用户信息测试数据服务类
// 数据表：userinfo
// 作者：Lyevn
// 日期：2016/9/12 20:10:20
// 修改记录：
//***********************************************************************************

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
        /// <returns>Json字符串</returns>
        public static String CreateTestData()
        {
            UserInfo userInfo = new UserInfo();
            
            // 数据填充
            String str = String.Empty; 
            str = Guid.NewGuid().ToString("N");
            userInfo.UserName = "测试用户名" + str.Substring(0, 4);

            Random rd = new Random();
            userInfo.UserIntegral = rd.Next(1, 150);

            return JsonConvert.SerializeObject(userInfo);
        }
    }
}
