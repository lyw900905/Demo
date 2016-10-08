//***********************************************************************************
// 文件名称：Program.cs
// 功能描述：Interlocked测试控制台应用程序类
// 数据表：
// 作者：Lyevn
// 日期：2016/10/08 16:44:20
// 修改记录：
//***********************************************************************************

using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterlockedTest
{
    /// <summary>
    /// Interlocked测试类
    /// </summary>
    class Program
    {
        private static int mresult;

        /// <summary>
        /// 主函数入口方法 
        /// </summary>
        /// <param name="args">参数信息</param>
        public static void Main(string[] args)
        {
            while (true)
            {
                Task[] tasks = new Task[100];
                int i = 0;

                for (i = 0; i < tasks.Length; i++)
                {
                    //开启线程调用
                    tasks[i] = Task.Factory.StartNew((num) =>
                    {
                        var taskid = (int)num;

                        Work(taskid);
                    }, i);
                }

                Task.WaitAll(tasks);

                //打印输出
                Console.WriteLine(mresult);

                Console.ReadKey();
            }
        }

        /// <summary>
        /// 线程调用方法
        /// </summary>
        /// <param name="taskId"></param>
        private static void Work(int taskId)
        {
            for (int i = 0; i < 10; i++)
            {
                // 不采用Interlocked锁定值进行变量递增
                //mresult++;

                // 采用Interlocked锁定值进行递增
                Interlocked.Increment(ref mresult);
            }
        }
    }
}
