//***********************************************************************************
// 文件名称：From1.cs
// 功能描述：监听服务界面开启
// 数据表：
// 作者：Lyevn
// 日期：2016/9/12 20:10:20
// 修改记录：
//***********************************************************************************

using System;
using System.Windows.Forms;

namespace ListenServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 开启服务器监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Open_Click(object sender, EventArgs e)
        {
            THttpListener.Instance.StartServerListener();
        }
    }
}
