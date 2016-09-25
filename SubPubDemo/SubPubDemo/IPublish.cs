/********************************************************************************
** auth： lyw
** date： 2016-09-12
** desc： 发布接口
** Ver.:  V1.0.0
*********************************************************************************/
using System;

namespace SubPubDemo
{
    /// <summary>
    ///  定义发布接口
    /// </summary>
    public interface IPublish
    {
        /// <summary>
        /// 订阅方法
        /// </summary>
        /// <param name="dealAction">委托方法</param>
        void Subscribe(Action<String> dealAction);

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="msg">信息</param>
        void Publish(String msg);
    }
}
