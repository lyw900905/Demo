/********************************************************************************
** auth： lyw
** date： 2016-09-12
** desc： 用户操作类
** Ver.:  V1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;

namespace Common.DAL
{
    using Common.Entity;
    using Common.Helper;
    using MySql.Data.MySqlClient;

    public static class UserInfoDAL
    {
        /// <summary>
        /// 查询所有用户信息
        /// </summary>
        /// <returns>返回用户信息列表</returns>
        public static List<UserInfo> QueryAllUserInfo()
        {
            List<UserInfo> userList = new List<UserInfo>();
            string sqlstr = "select * from UserInfo";
            try
            {
                DataTable dt = MySQLHelper.GetDataTable(sqlstr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
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
                throw ex;
            }

            return userList;
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="userInfo">用户实体信息</param>
        /// <returns>返回结果id</returns>
        public static int AddUserInfo(UserInfo userInfo)
        {
            object obj = null;
            try
            {
                string sqlstr = "Insert into UserInfo(UserName,UserIntegral) values(@UserName, @UserIntegral);select @@identity;";

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
