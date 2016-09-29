//***********************************************************************************
// 文件名称：ClientFrom.cs
// 功能描述：请求服务器监听测试界面
// 数据表：
// 作者：Lyevn
// 日期：2016/9/12 20:10:20
// 修改记录：
//***********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TestHttp
{
    using Common.Entity;
    using Common.Servers;

    /// <summary>
    /// 客户端请求监听界面后台代码类
    /// </summary>
    public partial class ClientFrom : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ClientFrom()
        {
            InitializeComponent();

            // 订阅更新数据方法
            HttpClientTest.Instance.Subscribe(UpdateUserInfo);
        }

        /// <summary>
        /// 添加测试数据
        /// </summary>
        /// <param name="sender">按钮对象</param>
        /// <param name="e">按钮对象事件参数信息</param>
        private void button1_Click(object sender, EventArgs e)
        {
            // 生成随机数据并发送至监听服务器
            String teststr = TestDataService.CreateTestData();
            HttpClientTest.Instance.SendRequestMsg(teststr);
        }

        /// <summary>
        /// 开启客户端timer查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            // 开始timer查询更新数据
            HttpClientTest.Instance.StartConnetQueryTimer();
        }

        /// <summary>
        /// 更新界面显示
        /// </summary>
        /// <param name="userInfoList">信息列表</param>
        private void UpdateUserInfo(List<UserInfo> userInfoList)
        {
            // 刷新界面listView显示
            this.Invoke(new Action(delegate
            {
                userInfoList = userInfoList.OrderByDescending(o => o.UserIntegral).ToList();
                
                // listView界面属性设置
                lstViewInfo.Items.Clear();
                lstViewInfo.Columns.Clear();
                lstViewInfo.GridLines = false;
                lstViewInfo.FullRowSelect = true;
                lstViewInfo.View = View.Details;
                lstViewInfo.MultiSelect = false;
                lstViewInfo.HeaderStyle = ColumnHeaderStyle.None;

                // listView添加列
                lstViewInfo.Columns.Add("名次", 40);
                lstViewInfo.Columns.Add("用户名", 160);
                lstViewInfo.Columns.Add("积分", 50);

                int count = 0;
                foreach (var item in userInfoList)
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
        /// <param name="sender">按钮对象</param>
        /// <param name="e">按钮对象事件参数信息</param>
        private void btn_listener_Click(object sender, EventArgs e)
        {
            // 开始客户端监听
            HttpClientTest.Instance.StartClientListener();
        }

        /// <summary>
        /// 停止客户端监听
        /// </summary>
        /// <param name="sender">按钮对象</param>
        /// <param name="e">按钮对象事件参数信息</param>
        private void btn_Stop_Click(object sender, EventArgs e)
        {
            // 停止客户端监听
            HttpClientTest.Instance.StopClientListener();
        }
    }
}
