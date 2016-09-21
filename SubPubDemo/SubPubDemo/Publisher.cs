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
        /// 定义事件
        /// </summary>
        private event Action<String> publishEvent;

        /// <summary>
        /// 订阅方法
        /// </summary>
        /// <param name="dealAction">委托方法</param>
        public void Subscriber(Action<String> dealAction)
        {
            if (publishEvent != null && publishEvent.GetInvocationList().Length > 0)
            {
                foreach (var item in publishEvent.GetInvocationList())
                {
                    if (item.Equals(dealAction))
                    {
                        return;
                    }
                }
            }
            publishEvent += dealAction;
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="dealAction"></param>
        public void DelSubscriber(Action<String> dealAction) 
        {
            publishEvent -= dealAction;
        }

        /// <summary>
        /// 发布信息
        /// </summary>
        /// <param name="msg">信息内容</param>
        public void Publish(String msg)
        {
            if (publishEvent != null)
            {
                publishEvent(msg);
            }
        }
    }
}
