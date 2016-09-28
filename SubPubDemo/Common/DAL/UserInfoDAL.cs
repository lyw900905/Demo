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
            List<UserInfo> userList = new List<UserInfo>();
            String sqlstr = "select * from UserInfo ORDER BY UserIntegral DESC LIMIT 20";
            try
            {
                // 数据获取
                DataTable dt = MySQLHelper.GetDataTable(sqlstr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        // 用户信息设置
                        UserInfo info = new UserInfo
                        {
                            UserId = Convert.ToInt32(row["UserId"]),
                            UserName = row["UserName"].ToString(),
                            UserIntegral = Convert.ToInt32(row["UserIntegral"])
                        };

                        userList.Add(info);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("查询所有用户信息异常：" + ex);
                return null;
            }

            return userList;
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="userInfo">用户实体信息</param>
        /// <returns>返回结果id</returns>
        public static Int32 AddUserInfo(UserInfo userInfo)
        {
            Object obj = null;
            try
            {
                // Sql指令组织
                String sqlstr = "Insert into UserInfo(UserName,UserIntegral) values(@UserName, @UserIntegral);select @@identity;";
                MySqlParameter[] para = new MySqlParameter[]
                {
                    new MySqlParameter("@UserName",userInfo.UserName),
                    new MySqlParameter("@UserIntegral",userInfo.UserIntegral)
                };

                obj = MySQLHelper.ExecuteScalar(sqlstr, para);
                if (obj != null)
                {
                    userInfo.UserId = Convert.ToInt32(obj);
                }

                return userInfo.UserId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
