//***********************************************************************************
// 文件名称：ThreadTest.cs
// 功能描述：Thread测试类
// 数据表：
// 作者：Lyevn
// 日期：2016/10/08 09:52:20
// 修改记录：
//***********************************************************************************

using System;
using System.Threading;
using System.Windows.Forms;

namespace ThreadDemo
{
    /// <summary>
    /// Thread测试类
    /// </summary>
    public class ThreadTest
    {
        /// <summary>
        /// 线程操作锁
        /// </summary>
        private static Object mMonitorObject = new Object();

        /// <summary>
        /// 开始测试
        /// </summary>
        public static void StartTest()
        {
            // 线程threadTest1初始化参数信息
            Thread threadTest1 = new Thread(new ThreadStart(TestMethod));
            threadTest1.Name = "threadtest1";
            threadTest1.IsBackground = true;

            // 线程threadTest2初始化参数信息
            Thread threadTest2 = new Thread(new ThreadStart(TestMethod));
            threadTest2.Name = "threadtest2";
            threadTest2.IsBackground = true;

            // 启动线程threadTest1
            threadTest1.Start();

            // 启动线程threadTest2
            threadTest2.Start();

            //阻塞线程
            threadTest1.Join();
            threadTest2.Join();

            Console.Read();
        }

        /// <summary>
        /// 开始测试，带一个String型参数
        /// </summary>
        /// <param name="message">显示信息</param>
        public static void StartTest(String message)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(TestMethod));
            thread.IsBackground = true;

            thread.Start(message);
        }

        /// <summary>
        /// Thread测试方法
        /// </summary>
        private static void TestMethod()
        {
            //MessageBox.Show("Thread无参数测试", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);

            if (!Monitor.TryEnter(mMonitorObject))
            {
                Console.WriteLine("Can't visit Object " + Thread.CurrentThread.Name);

                return;
            }

            try
            {
                Monitor.Enter(mMonitorObject);
                Console.WriteLine("Enter Monitor " + Thread.CurrentThread.Name);

                Thread.Sleep(5000);
            }
            finally
            {
                Monitor.Exit(mMonitorObject);
            }
        }

        /// <summary>
        /// Thread测试方法,带一个参数
        /// </summary>
        /// <param name="obj">显示信息</param>
        private static void TestMethod(Object obj)
        {
            String messageString = obj as String;

            MessageBox.Show("Thread带参数测试:" + messageString, "提示");
        }
    }
}
