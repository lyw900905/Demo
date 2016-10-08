namespace ThreadDemo
{
    partial class ThreadDemo
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
            this.btn_Thread = new System.Windows.Forms.Button();
            this.btn_Hello = new System.Windows.Forms.Button();
            this.btn_Task = new System.Windows.Forms.Button();
            this.btn_Await = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Thread
            // 
            this.btn_Thread.Location = new System.Drawing.Point(33, 27);
            this.btn_Thread.Name = "btn_Thread";
            this.btn_Thread.Size = new System.Drawing.Size(58, 23);
            this.btn_Thread.TabIndex = 0;
            this.btn_Thread.Text = "Thread";
            this.btn_Thread.UseVisualStyleBackColor = true;
            this.btn_Thread.Click += new System.EventHandler(this.btn_Thread_Click);
            // 
            // btn_Hello
            // 
            this.btn_Hello.Location = new System.Drawing.Point(107, 27);
            this.btn_Hello.Name = "btn_Hello";
            this.btn_Hello.Size = new System.Drawing.Size(55, 23);
            this.btn_Hello.TabIndex = 1;
            this.btn_Hello.Text = "hello";
            this.btn_Hello.UseVisualStyleBackColor = true;
            this.btn_Hello.Click += new System.EventHandler(this.btn_Hello_Click);
            // 
            // btn_Task
            // 
            this.btn_Task.Location = new System.Drawing.Point(33, 78);
            this.btn_Task.Name = "btn_Task";
            this.btn_Task.Size = new System.Drawing.Size(58, 23);
            this.btn_Task.TabIndex = 2;
            this.btn_Task.Text = "Task";
            this.btn_Task.UseVisualStyleBackColor = true;
            this.btn_Task.Click += new System.EventHandler(this.btn_Task_Click);
            // 
            // btn_Await
            // 
            this.btn_Await.Location = new System.Drawing.Point(33, 124);
            this.btn_Await.Name = "btn_Await";
            this.btn_Await.Size = new System.Drawing.Size(58, 23);
            this.btn_Await.TabIndex = 3;
            this.btn_Await.Text = "Await";
            this.btn_Await.UseVisualStyleBackColor = true;
            this.btn_Await.Click += new System.EventHandler(this.btn_Await_Click);
            // 
            // ThreadDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 271);
            this.Controls.Add(this.btn_Await);
            this.Controls.Add(this.btn_Task);
            this.Controls.Add(this.btn_Hello);
            this.Controls.Add(this.btn_Thread);
            this.Name = "ThreadDemo";
            this.Text = "多线程测试";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Thread;
        private System.Windows.Forms.Button btn_Hello;
        private System.Windows.Forms.Button btn_Task;
        private System.Windows.Forms.Button btn_Await;
    }
}

