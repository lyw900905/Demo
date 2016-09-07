using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubPubDemo
{
    using Common.Interface;

    /// <summary>
    /// 订阅者
    /// </summary>
    public class Subscriber
    {
        private string _subscriberName;

        /// <summary>
        /// 添加订阅
        /// </summary>
        public ISubscribe AddSubscribe
        {
            set
            {
                value.SubscribeEvent += Show;
            }
        }

        /// <summary>
        /// 移出订阅
        /// </summary>
        public ISubscribe RemobeSubscribe
        {
            set
            {
                value.SubscribeEvent -= Show;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="subscriberName">订阅者名称</param>
        public Subscriber(string subscriberName)
        {
            this._subscriberName = subscriberName;
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="str">显示内容</param>
        private void Show(string str)
        {
            Console.WriteLine(string.Format("我是{0},我收到订阅的消息是：{1}", _subscriberName, str));
        }
    }
}
