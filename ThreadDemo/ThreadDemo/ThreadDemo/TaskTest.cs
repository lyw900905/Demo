//***********************************************************************************
// 文件名称：TaskTest.cs
// 功能描述：TaskTest测试类
// 数据表：
// 作者：Lyevn
// 日期：2016/10/08 11:32:20
// 修改记录：
//***********************************************************************************

using System;
using System.Threading.Tasks;

namespace ThreadDemo
{
    /// <summary>
    /// Task测试类
    /// </summary>
    public class TaskTest
    {
        /// <summary>
        /// 委托方法
        /// </summary>
        private static Action<Object> action = new Action<object>(TestMethod);

        /// <summary>
        /// 开始测试Task
        /// </summary>
        public static void StartTest()
        {
            Task task = new Task(action, "Hello");

            task.Start();
        }

        /// <summary>
        /// 测试方法
        /// </summary>
        /// <param name="obj">对象参数</param>
        private static void TestMethod(Object obj)
        {
            String message = obj as String;

            Console.WriteLine("收到的数据：" + message);
        }
    }
}
