// todo：此处缺少文件头信息
using System;
//// todo:没有移除多余无用using
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
        //todo:注意代码注释，还有字段命名，属性和非静态字段不能使用下划线开头，注意命名规范
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
