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
    ///  定义发布委托
    /// </summary>
    /// <param name="content">内容</param>
    public delegate void PublishHandle(String content);

    /// <summary>
    ///  定义发布接口
    /// </summary>
    public interface IPublish
    {
        /// <summary>
        /// 更新
        /// </summary>
        void Notify();
    }
}
