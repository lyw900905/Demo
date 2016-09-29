//***********************************************************************************
// 文件名称：Subscriber.cs
// 功能描述：订阅者
// 数据表：
// 作者：Lyevn
// 日期：2016/9/12 20:10:20
// 修改记录：
//***********************************************************************************

using System;

namespace SubPubDemo
{
    /// <summary>
    /// 订阅者
    /// </summary>
    public class Subscriber
    {
        /// <summary>
        /// 订阅名称
        /// </summary>
        private String subscriberName;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>
        public Subscriber(String name)
        {
            subscriberName = name;
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="content">发布内容</param>
        public void ShowContent(String content)
        {
            Console.WriteLine(String.Format("我是{0},我收到订阅的消息是{1}", subscriberName, content));
        }
    }
}
