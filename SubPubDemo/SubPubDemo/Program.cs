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
            //// 新建两个订阅器
            //SubPubComponet subPubCompont1 = new SubPubComponet("订阅器1");
            //SubPubComponet subPubCompont2 = new SubPubComponet("订阅器2");

            //// 新建两个发布者
            //IPublish publisher1 = new Publisher("Author1");
            //IPublish publisher2 = new Publisher("Author2");


            //// 与订阅器关联
            //publisher1.PublishEvent += subPubCompont1.PublishEvent;
            //publisher2.PublishEvent += subPubCompont2.PublishEvent;
            //publisher1.PublishEvent += subPubCompont2.PublishEvent;

            //// 新建两个订阅者
            //Subscriber s1 = new Subscriber("订阅人1");
            //Subscriber s2 = new Subscriber("订阅人2");


            //// 进行订阅
            //s1.AddSubscribe = subPubCompont1;
            //s1.AddSubscribe = subPubCompont2;
            //s2.AddSubscribe = subPubCompont2;

            //// 发布者发布消息
            //publisher1.Notify("新闻1");
            //publisher2.Notify("新闻2");


            //Console.WriteLine("");

            //// s1取消对订阅器2的订阅
            //s1.RemobeSubscribe = subPubCompont2;

            //// 发布者发布消息
            //publisher1.Notify("新闻3");
            //publisher2.Notify("新闻4");

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
