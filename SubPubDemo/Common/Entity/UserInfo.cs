/********************************************************************************
** auth： lyw
** date： 2016-09-12
** desc： 用户实体类
** Ver.:  V1.0.0
*********************************************************************************/

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
