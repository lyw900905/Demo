//***********************************************************************************
// 文件名称：Program.cs
// 功能描述：订阅发布控制台主程序
// 数据表：
// 作者：Lyevn
// 日期：2016/9/12 20:10:20
// 修改记录：
//***********************************************************************************

using System;

namespace SubPubDemo
{
    /// <summary>
    /// 订阅发布控制台类
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 主函数
        /// </summary>
        /// <param name="args">参数信息</param>
        public static void Main(string[] args)
        {
            // 初始化
            Publisher publisher = new Publisher();
            String subscriberName1 = "订阅者1";
            String subscriberName2 = "订阅者2";
            Subscriber subscriber1 = new Subscriber(subscriberName1);
            Subscriber subscriber2 = new Subscriber(subscriberName2);

            // 订阅发布信息
            publisher.Subscribe(subscriberName1, subscriber1.ShowContent);
            publisher.Subscribe(subscriberName2, subscriber2.ShowContent);

            // 发布一条消息
            publisher.Publish("发布第一条消息");

            // 取消订阅者2的订阅
            publisher.DeleteSubscribe(subscriberName2, subscriber2.ShowContent);

            // 发布第二条消息
            publisher.Publish("发布第二条信息");

            Console.ReadKey();
        }
    }
}
