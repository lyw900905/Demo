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
            // UserInfo 用户名采用“用户名”+ Guid前四位
            // 用户积分采用1到150内的随机数
            UserInfo userInfo = new UserInfo();
            
            // 用户名数据填充
            String strGuid = String.Empty; 
            strGuid = Guid.NewGuid().ToString("N");
            userInfo.UserName = "用户名" + strGuid.Substring(0, 4);

            // 生成用户随机积分
            Random random = new Random();
            userInfo.UserIntegral = random.Next(1, 150);

            return JsonConvert.SerializeObject(userInfo);
        }
    }
}
