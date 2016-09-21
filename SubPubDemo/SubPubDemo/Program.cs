/********************************************************************************
** auth： lyw
** date： 2016-09-12
** desc： 客户端调用
** Ver.:  V1.0.0
*********************************************************************************/
using System;

namespace SubPubDemo
{
    class Program
    {
        static void Main(string[] args)
        { 
            Publisher news = new Publisher();
            Subscriber au1 = new Subscriber("订阅者1");
            Subscriber au2 = new Subscriber("订阅者2");

            //news.PublishEvent+=new PublishHandle(au1.Show);
            //news.PublishEvent+=new PublishHandle(au2.Show);
            //news.PubContent = "发布第一条新闻";
            //news.Notify();

            news.Subscriber(au1.Show);
            news.Subscriber(au2.Show);
            news.Publish("发布第一条消息");

            news.DelSubscriber(au2.Show);
            news.Publish("发布第二条信息");

            Console.ReadKey();
        }
    }
}
