/********************************************************************************
** auth： lyw
** date： 2016-09-12
** desc： 订阅者实现
** Ver.:  V1.0.0
*********************************************************************************/
using System;

namespace SubPubDemo
{
    /// <summary>
    /// 订阅者
    /// </summary>
    public class Subscriber
    {
        /// <summary>
        /// 名称
        /// </summary>
        private string subsName;

        /// <summary>
        /// 订阅者
        /// </summary>
        private IPublish subscriber;

        ///// <summary>
        ///// 添加订阅
        ///// </summary>
        //public ISubscribe AddSubscribe
        //{
        //    set
        //    {
        //        value.SubscribeEvent += Show;
        //    }
        //}

        ///// <summary>
        ///// 移出订阅
        ///// </summary>
        //public ISubscribe RemobeSubscribe
        //{
        //    set
        //    {
        //        value.SubscribeEvent -= Show;
        //    }
        //}

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="sub">订阅内容</param>
        public Subscriber(string name, IPublish sub)
        {
            this.subsName = name;
            this.subscriber = sub;
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="str">显示内容</param>
        public void Show()
        {
            Console.WriteLine(string.Format("我是{0},我收到订阅的消息是{1}", subsName, subscriber.PubContent));
        }
    }
}
