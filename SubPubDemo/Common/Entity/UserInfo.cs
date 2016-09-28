//***********************************************************************************
// 文件名称：UserInfo.cs
// 功能描述：用户信息实体类
// 数据表：userinfo
// 作者：Lyevn
// 日期：2016/9/12 20:10:20
// 修改记录：
//***********************************************************************************

using System.Runtime.Serialization;

namespace Common.Entity
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [DataContract]
    public class UserInfo
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 用户积分
        /// </summary>
        [DataMember]
        public int UserIntegral { get; set; }
    }
}
