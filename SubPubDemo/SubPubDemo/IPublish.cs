/********************************************************************************
** auth： lyw
** date： 2016-09-12
** desc： 发布接口
** Ver.:  V1.0.0
*********************************************************************************/

namespace SubPubDemo
{
    // 定义发布事件
    public delegate void PublishHandle();

    /// <summary>
    ///  定义发布接口
    /// </summary>
    public interface IPublish
    {
        /// <summary>
        /// 更新
        /// </summary>
        void Notify();
        
        /// <summary>
        /// 发布内容
        /// </summary>
        string PubContent { get; set; }
    }
}
