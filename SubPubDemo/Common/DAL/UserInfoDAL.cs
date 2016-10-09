//***********************************************************************************
// 文件名称：UserInfoDAL.cs
// 功能描述：用户操作数据库服务类
// 数据表：userinfo
// 作者：Lyevn
// 日期：2016/9/12 20:10:20
// 修改记录：
//***********************************************************************************

using System;
using System.Data;
using System.Collections.Generic;

namespace Common.DAL
{
    using Common.Entity;
    using Common.Helper;
    using MySql.Data.MySqlClient;

    /// <summary>
    /// 用户操作数据库服务类
    /// </summary>
    public static class UserInfoDAL
    {
        /// <summary>
        /// 查询所有用户信息
        /// </summary>
        /// <returns>返回用户信息列表</returns>
        public static List<UserInfo> QueryAllUserInfo()
        {
            List<UserInfo> userInfoList = new List<UserInfo>();
            String sqlString = "SELECT * FROM UserInfo ORDER BY UserIntegral DESC LIMIT 20";

            // 数据获取
            DataTable dataTable = MySQLHelper.GetDataTable(sqlString);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    // 用户信息设置
                    UserInfo userInfo = new UserInfo
                    {
                        UserId = Convert.ToInt32(dataRow["UserId"]),
                        UserName = dataRow["UserName"].ToString(),
                        UserIntegral = Convert.ToInt32(dataRow["UserIntegral"])
                    };

                    userInfoList.Add(userInfo);
                }
            }

            return userInfoList;
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="userInfo">用户实体信息</param>
        /// <returns>返回结果id</returns>
        public static Int32 AddUserInfo(UserInfo userInfo)
        {
            Object obj = null;

            // Sql指令组织
            String sqlString = "INSERT INTO UserInfo(UserName,UserIntegral) VALUES(@UserName, @UserIntegral);SELECT @@identity;";
            MySqlParameter[] para = new MySqlParameter[]
            {
                new MySqlParameter("@UserName",userInfo.UserName),
                new MySqlParameter("@UserIntegral",userInfo.UserIntegral)
            };

            obj = MySQLHelper.ExecuteScalar(sqlString, para);
            if (obj != null)
            {
                userInfo.UserId = Convert.ToInt32(obj);
            }

            return userInfo.UserId;
        }
    }
}
