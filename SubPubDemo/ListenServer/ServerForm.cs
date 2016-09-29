//***********************************************************************************
// 文件名称：ServerForm.cs
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
    /// <summary>
    /// 服务器监听界面后台类
    /// </summary>
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 开启服务器监听
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">对象事件参数信息</param>
        private void btn_Open_Click(object sender, EventArgs e)
        {
            THttpListener.Instance.StartServerListener();
        }
    }
}
