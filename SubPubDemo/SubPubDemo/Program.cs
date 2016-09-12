// todo：此处缺少文件头信息
using System;
//// todo:没有移除多余无用using
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubPubDemo
{
    // todo:接口定义一般情况下应该不是在common项目里面
    using Common.Interface;

    class Program
    {
        static void Main(string[] args)
        { 
            // 新建两个订阅器
            SubPubComponet subPubCompont1 = new SubPubComponet("订阅器1");
            SubPubComponet subPubCompont2 = new SubPubComponet("订阅器2");

            // 新建两个发布者
            IPublish publisher1 = new Publisher("Author1");
            IPublish publisher2 = new Publisher("Author2");

            // 与订阅器关联
            publisher1.PublishEvent += subPubCompont1.PublishEvent;
            publisher2.PublishEvent += subPubCompont2.PublishEvent;
            publisher1.PublishEvent += subPubCompont2.PublishEvent;

            // 新建两个订阅者
            Subscriber s1 = new Subscriber("订阅人1");
            Subscriber s2 = new Subscriber("订阅人2");

            // 进行订阅
            s1.AddSubscribe = subPubCompont1;
            s1.AddSubscribe = subPubCompont2;
            s2.AddSubscribe = subPubCompont2;

            // 发布者发布消息
            publisher1.Notify("新闻1");
            publisher2.Notify("新闻2");

            Console.WriteLine("");

            // s1取消对订阅器2的订阅
            s1.RemobeSubscribe = subPubCompont2;

            // 发布者发布消息
            publisher1.Notify("新闻3");
            publisher2.Notify("新闻4");

            Console.ReadKey();
        }
    }
}
