/********************************************************************************
** auth： lyw
** date： 2016-09-12
** desc： 发布者实现
** Ver.:  V1.0.0
*********************************************************************************/

using System;

namespace SubPubDemo
{
    /// <summary>
    /// 发布者
    /// </summary>
    public class Publisher : IPublish
    {
        /// <summary>
        /// 内容
        /// </summary>
        private String pubContent;

        /// <summary>
        /// 发布内容
        /// </summary>
        public String PubContent
        {
            get
            {
                return pubContent;
            }
            set
            {
                pubContent = value;
            }
        }

        /// <summary>
        /// 发布通知事件
        /// </summary>
        public event PublishHandle publishEvent;
      
        /// <summary>
        /// 通知
        /// </summary>
        public void Notify()
        {
            if (publishEvent != null)
            {
                publishEvent(PubContent);
            }
            else
            {
                Console.WriteLine("无事件注册");
            }
        }
    }
}
