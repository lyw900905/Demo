using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestHttp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TestHttpClient.ConnectHttpServer();
            
        }

        string TestInfoJson() 
        {
            Common.Entity.UserInfo userInfo = new Common.Entity.UserInfo();
            userInfo.UserName = "测试用户名";
            userInfo.UserIntegral = 50;

            return Common.Helper.JSonHelper.Serialize(userInfo);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string info = TestInfoJson();
            TestHttpClient.SendMsg(info);
        }
    }
}
