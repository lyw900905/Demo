//***********************************************************************************
// 文件名称：ThreadDemo.cs
// 功能描述：多线程测试界面后台操作类
// 数据表：
// 作者：Lyevn
// 日期：2016/10/08 09:43:20
// 修改记录：
//***********************************************************************************

using System;
using System.Windows.Forms;

namespace ThreadDemo
{
    /// <summary>
    /// 多线程测试界面后台操作类
    /// </summary>
    public partial class ThreadDemo : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ThreadDemo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Thred测试按钮单击事件,不带参数
        /// </summary>
        /// <param name="sender">按钮单击事件对象</param>
        /// <param name="e">按钮单击事件参数信息</param>
        private void btn_Thread_Click(object sender, EventArgs e)
        {
            ThreadTest.StartTest();
        }

        /// <summary>
        /// Thred测试按钮单击事件,带一个参数
        /// </summary>
        /// <param name="sender">按钮单击事件对象</param>
        /// <param name="e">按钮单击事件参数信息</param>
        private void btn_Hello_Click(object sender, EventArgs e)
        {
            ThreadTest.StartTest("Hello");
        }

        /// <summary>
        /// Task按钮测试单击事件
        /// </summary>
        /// <param name="sender">按钮单击事件对象</param>
        /// <param name="e">按钮单击事件参数信息</param>
        private void btn_Task_Click(object sender, EventArgs e)
        {
            TaskTest.StartTest();
        }

        /// <summary>
        /// Await按钮测试单击事件
        /// </summary>
        /// <param name="sender">按钮单击事件对象</param>
        /// <param name="e">按钮单击事件参数信息</param>
        private void btn_Await_Click(object sender, EventArgs e)
        {
            TaskAsyncTest.StartTest();
        }
    }
}
