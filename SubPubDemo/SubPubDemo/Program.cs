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
            Subscriber au1 = new Subscriber("订阅者1", news);
            Subscriber au2 = new Subscriber("订阅者2", news);

            news.publishEvent+=new PublishHandle(au1.Show);
            news.publishEvent+=new PublishHandle(au2.Show);

            news.PubContent = "发布第一条新闻";
            news.Notify();

            Console.ReadKey();
        }
    }
}
