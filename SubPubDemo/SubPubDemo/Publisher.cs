//***********************************************************************************
// 文件名称：Publisher.cs
// 功能描述：发布者
// 数据表：
// 作者：Lyevn
// 日期：2016/9/12 20:10:20
// 修改记录：
//***********************************************************************************

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
        public void Subscribe(Action<String> dealAction)
        {
            // 判断是否已经存在要订阅方法事件
            if (publishEvent != null && publishEvent.GetInvocationList().Length > 0)
            {
                foreach (var action in publishEvent.GetInvocationList())
                {
                    if (action.Equals(dealAction))
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
        /// <param name="dealAction">委托方法</param>
        public void DelSubscribe(Action<String> dealAction)
        {
            // 判断是否存在要取消订阅的事件方法，存在则取消事件
            if (publishEvent != null && publishEvent.GetInvocationList().Length > 0)
            {
                foreach (var action in publishEvent.GetInvocationList())
                {
                    if (action.Equals(dealAction))
                    {
                        publishEvent -= dealAction;
                    }
                }
            }
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
