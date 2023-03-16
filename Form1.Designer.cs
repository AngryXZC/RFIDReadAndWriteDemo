namespace SampleServer
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Connect = new System.Windows.Forms.Button();
            this.Disconnect = new System.Windows.Forms.Button();
            this.InventoryBtn = new System.Windows.Forms.Button();
            this.stopInventoryBtn = new System.Windows.Forms.Button();
            this.writeBtn = new System.Windows.Forms.Button();
            this.inputSample = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(25, 48);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(198, 57);
            this.Connect.TabIndex = 0;
            this.Connect.Text = "连接读写器";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // Disconnect
            // 
            this.Disconnect.Location = new System.Drawing.Point(573, 48);
            this.Disconnect.Name = "Disconnect";
            this.Disconnect.Size = new System.Drawing.Size(198, 57);
            this.Disconnect.TabIndex = 1;
            this.Disconnect.Text = "断开读写器";
            this.Disconnect.UseVisualStyleBackColor = true;
            this.Disconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // InventoryBtn
            // 
            this.InventoryBtn.Location = new System.Drawing.Point(25, 141);
            this.InventoryBtn.Name = "InventoryBtn";
            this.InventoryBtn.Size = new System.Drawing.Size(198, 57);
            this.InventoryBtn.TabIndex = 2;
            this.InventoryBtn.Text = "盘存";
            this.InventoryBtn.UseVisualStyleBackColor = true;
            this.InventoryBtn.Click += new System.EventHandler(this.InventoryBtn_Click);
            // 
            // stopInventoryBtn
            // 
            this.stopInventoryBtn.Location = new System.Drawing.Point(573, 141);
            this.stopInventoryBtn.Name = "stopInventoryBtn";
            this.stopInventoryBtn.Size = new System.Drawing.Size(198, 57);
            this.stopInventoryBtn.TabIndex = 3;
            this.stopInventoryBtn.Text = "关闭盘点";
            this.stopInventoryBtn.UseVisualStyleBackColor = true;
            this.stopInventoryBtn.Click += new System.EventHandler(this.stopInventoryBtn_Click);
            // 
            // writeBtn
            // 
            this.writeBtn.Location = new System.Drawing.Point(505, 275);
            this.writeBtn.Name = "writeBtn";
            this.writeBtn.Size = new System.Drawing.Size(198, 57);
            this.writeBtn.TabIndex = 4;
            this.writeBtn.Text = "写入一个标签";
            this.writeBtn.UseVisualStyleBackColor = true;
            this.writeBtn.Click += new System.EventHandler(this.writeBtn_Click);
            // 
            // inputSample
            // 
            this.inputSample.Location = new System.Drawing.Point(78, 293);
            this.inputSample.Name = "inputSample";
            this.inputSample.Size = new System.Drawing.Size(348, 25);
            this.inputSample.TabIndex = 5;
            this.inputSample.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.inputSample_KeyPress);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.inputSample);
            this.Controls.Add(this.writeBtn);
            this.Controls.Add(this.stopInventoryBtn);
            this.Controls.Add(this.InventoryBtn);
            this.Controls.Add(this.Disconnect);
            this.Controls.Add(this.Connect);
            this.Name = "Form1";
            this.Text = "刷卡服务";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Button Disconnect;
        private System.Windows.Forms.Button InventoryBtn;
        private System.Windows.Forms.Button stopInventoryBtn;
        private System.Windows.Forms.Button writeBtn;
        private System.Windows.Forms.TextBox inputSample;
    }
}

