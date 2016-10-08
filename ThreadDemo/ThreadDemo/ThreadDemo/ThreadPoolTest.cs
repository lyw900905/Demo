//***********************************************************************************
// 文件名称：ThreadPoolTest.cs
// 功能描述：ThreadPool测试类
// 数据表：
// 作者：Lyevn
// 日期：2016/10/08 10:42:20
// 修改记录：
//***********************************************************************************

using System;
using System.Threading;
using System.Windows.Forms;

namespace ThreadDemo
{
    /// <summary>
    /// ThreadPool测试类
    /// </summary>
    public class ThreadPoolTest
    {
        /// <summary>
        /// 开始使用ThreadPool测试
        /// </summary>
        public static void StartTest()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(TestMethod));
        }

        /// <summary>
        /// ThreadPool测试调用方法
        /// </summary>
        /// <param name="obj">参数对象</param>
        private static void TestMethod(Object obj)
        {
            MessageBox.Show("ThreadPool 测试", "提示");
        }
    }
}
