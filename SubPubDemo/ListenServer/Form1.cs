using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ListenServer
{
    public partial class Form1 : Form
    {
        THttpListener _httpListener;
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {
            THttpListener.Instance.StartServerListener();
        }
    }
}
