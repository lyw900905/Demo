//***********************************************************************************
// 文件名称：Program.cs
// 功能描述：多线程操作应用程序主入口类
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
    /// 应用程序主入口类
    /// </summary>
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ThreadDemo());
        }
    }
}
