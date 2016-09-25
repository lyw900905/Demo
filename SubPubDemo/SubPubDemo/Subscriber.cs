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
        private String subsName;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>
        public Subscriber(String name)
        {
            this.subsName = name;
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="content">发布内容</param>
        public void Show(String content)
        {
            Console.WriteLine(String.Format("我是{0},我收到订阅的消息是{1}", subsName, content));
        }
    }
}
