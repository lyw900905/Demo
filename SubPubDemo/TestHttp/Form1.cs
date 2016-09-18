﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TestHttp
{
    using Common.Entity;
    using Common.Helper;

    public partial class Form1 : Form
    {
        Encoding encoding = Encoding.UTF8;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TestHttpClient.ConnectHttpServer();
            Stream stream = TestHttpClient.SendMsg(TestInfoJson());
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
            Stream stream = null;
            stream = TestHttpClient.GetMsg();

            StreamReader strReader = new StreamReader(stream, encoding);
            string json = strReader.ReadToEnd();
            var ls = JSonHelper.Deserialize<List<UserInfo>>(json);
            UpdateListUser(ls);
        }

        /// <summary>
        /// 更新界面显示
        /// </summary>
        /// <param name="lstUser"></param>
        private void UpdateListUser(List<UserInfo> lstUser) 
        {
            lstUser = lstUser.OrderByDescending(o => o.UserIntegral).ToList();
            lstViewInfo.Items.Clear();
            lstViewInfo.Columns.Clear();
            lstViewInfo.GridLines = false;
            lstViewInfo.FullRowSelect = true;
            lstViewInfo.View = View.Details;
            lstViewInfo.MultiSelect = false;
            lstViewInfo.HeaderStyle = ColumnHeaderStyle.None;

            lstViewInfo.Columns.Add("名次",40);
            lstViewInfo.Columns.Add("用户名",100);
            lstViewInfo.Columns.Add("积分",50);
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
        }
    }
}
