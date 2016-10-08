namespace TestHttp
{
    partial class ClientFrom
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lstViewInfo = new System.Windows.Forms.ListView();
            this.btn_listener = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(54, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "添加测试数据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(54, 40);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "开启查询";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lstViewInfo
            // 
            this.lstViewInfo.Location = new System.Drawing.Point(54, 69);
            this.lstViewInfo.Name = "lstViewInfo";
            this.lstViewInfo.Size = new System.Drawing.Size(281, 344);
            this.lstViewInfo.TabIndex = 3;
            this.lstViewInfo.UseCompatibleStateImageBehavior = false;
            this.lstViewInfo.View = System.Windows.Forms.View.Details;
            // 
            // btn_listener
            // 
            this.btn_listener.Location = new System.Drawing.Point(278, 12);
            this.btn_listener.Name = "btn_listener";
            this.btn_listener.Size = new System.Drawing.Size(57, 23);
            this.btn_listener.TabIndex = 4;
            this.btn_listener.Text = "监听";
            this.btn_listener.UseVisualStyleBackColor = true;
            //this.btn_listener.Click += new System.EventHandler(this.btn_listener_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(278, 42);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(57, 23);
            this.btn_Stop.TabIndex = 5;
            this.btn_Stop.Text = "停止";
            this.btn_Stop.UseVisualStyleBackColor = true;
            //this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 425);
            //this.Controls.Add(this.btn_Stop);
            //this.Controls.Add(this.btn_listener);
            this.Controls.Add(this.lstViewInfo);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListView lstViewInfo;
        private System.Windows.Forms.Button btn_listener;
        private System.Windows.Forms.Button btn_Stop;
    }
}

