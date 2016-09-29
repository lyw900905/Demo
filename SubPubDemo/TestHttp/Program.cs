//***********************************************************************************
// 文件名称：Program.cs
// 功能描述：客户端应用程序主入口
// 数据表：
// 作者：Lyevn
// 日期：2016/9/12 20:10:20
// 修改记录：
//***********************************************************************************

using System;
using System.Windows.Forms;

namespace TestHttp
{
    /// <summary>
    /// 客户端应用程序主入口类
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
            Application.Run(new ClientFrom());
        }
    }
}
