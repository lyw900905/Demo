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
        string PubContent { get; set; }//todo:此属性有和作用？是不是多余的，而且使用此属性也是有问题的
    }
}
