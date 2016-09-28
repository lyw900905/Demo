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
    class Program
    {
        /// <summary>
        /// 主函数
        /// </summary>
        /// <param name="args">参数信息</param>
        static void Main(string[] args)
        {
            // 初始化
            Publisher news = new Publisher();
            Subscriber au1 = new Subscriber("订阅者1");
            Subscriber au2 = new Subscriber("订阅者2");

            // 订阅发布信息
            news.Subscribe(au1.ShowContent);
            news.Subscribe(au2.ShowContent);

            news.Publish("发布第一条消息");

            news.DelSubscribe(au2.ShowContent);

            news.Publish("发布第二条信息");

            Console.ReadKey();
        }
    }
}
