//***********************************************************************************
// 文件名称：Program.cs
// 功能描述：服务器监听
// 数据表：
// 作者：Lyevn
// 日期：2016/9/12 20:10:20
// 修改记录：
//***********************************************************************************

using System;
using System.Windows.Forms;

namespace ListenServer
{
    /// <summary>
    /// 服务器监听应用程序入口类
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ServerForm());
        }
    }
}
