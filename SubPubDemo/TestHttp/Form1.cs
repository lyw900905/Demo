using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TestHttp
{
    using Common.Entity;
    using Common.Helper;
    using Common.Servers;
    using Newtonsoft.Json;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 添加测试数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            String teststr = TestDataService.CreateTestData();
            TestHttpClient.Instance.SendMsg(teststr);
            TestHttpClient.Instance.Subscriber(UpdateListUser);
        }

        /// <summary>
        /// 开启客户端timer查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            TestHttpClient.Instance.Subscriber(UpdateListUser);
            TestHttpClient.Instance.StartConnet();
        }

        /// <summary>
        /// 更新界面显示
        /// </summary>
        /// <param name="lstUser">信息列表</param>
        private void UpdateListUser(List<UserInfo> lstUser)
        {
            this.Invoke(new Action(delegate
            {
                lstUser = lstUser.OrderByDescending(o => o.UserIntegral).ToList();
                lstViewInfo.Items.Clear();
                lstViewInfo.Columns.Clear();
                lstViewInfo.GridLines = false;
                lstViewInfo.FullRowSelect = true;
                lstViewInfo.View = View.Details;
                lstViewInfo.MultiSelect = false;
                lstViewInfo.HeaderStyle = ColumnHeaderStyle.None;

                lstViewInfo.Columns.Add("名次", 40);
                lstViewInfo.Columns.Add("用户名", 160);
                lstViewInfo.Columns.Add("积分", 50);
                int count = 0;
                foreach (var item in lstUser)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.SubItems.Clear();
                    count++;
                    lvi.Text = count.ToString();
                    lvi.SubItems.Add(item.UserName);
                    lvi.SubItems.Add(item.UserIntegral.ToString());

                    lstViewInfo.Items.Add(lvi);

                }

            }));
        }

        /// <summary>
        /// 开启客户端监听测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_listener_Click(object sender, EventArgs e)
        {
            TestHttpClient.Instance.StartClientListener();
        }
    }
}
