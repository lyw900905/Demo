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
        /// <param name="sub">订阅内容</param>//todo:注意参数注释
        public Subscriber(String name)
        {
            this.subsName = name;
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="str">显示内容</param>//todo:注意参数注释
        public void Show(String content)
        {
            Console.WriteLine(String.Format("我是{0},我收到订阅的消息是{1}", subsName, content));
        }
    }
}
