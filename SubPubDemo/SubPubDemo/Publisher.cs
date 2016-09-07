using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubPubDemo
{
    using Common.Interface;

    /// <summary>
    /// 发布者
    /// </summary>
    public class Publisher : IPublish
    {
        private string _publisherName;

        private event PublishHandle publishEvent;

        event PublishHandle IPublish.PublishEvent
        {
            add { publishEvent += value; }
            remove { publishEvent -= value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="publisherName">发布者名称</param>
        public Publisher(string publisherName)
        {
            _publisherName = publisherName;
        }

        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="str">通知信息</param>
        public void Notify(string str)
        {
            if (publishEvent != null)
            {
                publishEvent.Invoke(string.Format("我是{0},我发布{1}消息", _publisherName, str));
            }
        }
    }
}
