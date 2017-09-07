namespace POSApplication.UI
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnCash = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.lbWelcome = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.btnJob = new System.Windows.Forms.Button();
            this.lbStaff = new System.Windows.Forms.Label();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.btnSell = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbJH = new System.Windows.Forms.Label();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnReboot = new System.Windows.Forms.Button();
            this.btnShutdown = new System.Windows.Forms.Button();
            this.btnReceipt = new System.Windows.Forms.Button();
            this.btnRefund = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCash
            // 
            this.btnCash.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnCash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCash.Font = new System.Drawing.Font("宋体", 33F, System.Drawing.FontStyle.Bold);
            this.btnCash.ForeColor = System.Drawing.SystemColors.Window;
            this.btnCash.Location = new System.Drawing.Point(35, 356);
            this.btnCash.Name = "btnCash";
            this.btnCash.Size = new System.Drawing.Size(229, 99);
            this.btnCash.TabIndex = 1;
            this.btnCash.Text = "解款打印";
            this.btnCash.UseVisualStyleBackColor = false;
            this.btnCash.Click += new System.EventHandler(this.btnCash_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.BackColor = System.Drawing.Color.ForestGreen;
            this.btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpload.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold);
            this.btnUpload.ForeColor = System.Drawing.SystemColors.Window;
            this.btnUpload.Location = new System.Drawing.Point(270, 251);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(110, 204);
            this.btnUpload.TabIndex = 3;
            this.btnUpload.Text = "数据上传";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.BackColor = System.Drawing.Color.Indigo;
            this.btnConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfig.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold);
            this.btnConfig.ForeColor = System.Drawing.SystemColors.Window;
            this.btnConfig.Location = new System.Drawing.Point(386, 251);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(110, 204);
            this.btnConfig.TabIndex = 4;
            this.btnConfig.Text = "系统管理";
            this.btnConfig.UseVisualStyleBackColor = false;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // lbWelcome
            // 
            this.lbWelcome.AutoSize = true;
            this.lbWelcome.Font = new System.Drawing.Font("宋体", 35F, System.Drawing.FontStyle.Bold);
            this.lbWelcome.ForeColor = System.Drawing.SystemColors.Window;
            this.lbWelcome.Location = new System.Drawing.Point(27, 34);
            this.lbWelcome.Name = "lbWelcome";
            this.lbWelcome.Size = new System.Drawing.Size(164, 47);
            this.lbWelcome.TabIndex = 8;
            this.lbWelcome.Text = "便捷通";
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("宋体", 15F);
            this.lbTime.ForeColor = System.Drawing.SystemColors.Window;
            this.lbTime.Location = new System.Drawing.Point(449, 558);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(269, 20);
            this.lbTime.TabIndex = 7;
            this.lbTime.Text = "2014年12月31日12点31分00秒";
            // 
            // btnJob
            // 
            this.btnJob.BackColor = System.Drawing.Color.Orange;
            this.btnJob.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJob.Font = new System.Drawing.Font("宋体", 33F, System.Drawing.FontStyle.Bold);
            this.btnJob.ForeColor = System.Drawing.SystemColors.Window;
            this.btnJob.Location = new System.Drawing.Point(270, 146);
            this.btnJob.Name = "btnJob";
            this.btnJob.Size = new System.Drawing.Size(226, 99);
            this.btnJob.TabIndex = 2;
            this.btnJob.Text = "挂单管理";
            this.btnJob.UseVisualStyleBackColor = false;
            this.btnJob.Click += new System.EventHandler(this.btnJob_Click);
            // 
            // lbStaff
            // 
            this.lbStaff.AutoSize = true;
            this.lbStaff.Font = new System.Drawing.Font("宋体", 15F);
            this.lbStaff.ForeColor = System.Drawing.SystemColors.Window;
            this.lbStaff.Location = new System.Drawing.Point(556, 61);
            this.lbStaff.Name = "lbStaff";
            this.lbStaff.Size = new System.Drawing.Size(129, 20);
            this.lbStaff.TabIndex = 6;
            this.lbStaff.Text = "员工：凌凌漆";
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.Color.SteelBlue;
            this.btnLogOut.Font = new System.Drawing.Font("宋体", 15F);
            this.btnLogOut.ForeColor = System.Drawing.SystemColors.Window;
            this.btnLogOut.Location = new System.Drawing.Point(710, 41);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(70, 40);
            this.btnLogOut.TabIndex = 5;
            this.btnLogOut.Text = "注销";
            this.btnLogOut.UseVisualStyleBackColor = false;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // btnSell
            // 
            this.btnSell.BackColor = System.Drawing.Color.Maroon;
            this.btnSell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSell.Font = new System.Drawing.Font("宋体", 50F, System.Drawing.FontStyle.Bold);
            this.btnSell.ForeColor = System.Drawing.SystemColors.Window;
            this.btnSell.Location = new System.Drawing.Point(35, 146);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(229, 204);
            this.btnSell.TabIndex = 0;
            this.btnSell.Text = "销售收银";
            this.btnSell.UseVisualStyleBackColor = false;
            this.btnSell.Click += new System.EventHandler(this.btnSell_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbJH
            // 
            this.lbJH.AutoSize = true;
            this.lbJH.Font = new System.Drawing.Font("宋体", 15F);
            this.lbJH.ForeColor = System.Drawing.SystemColors.Window;
            this.lbJH.Location = new System.Drawing.Point(556, 34);
            this.lbJH.Name = "lbJH";
            this.lbJH.Size = new System.Drawing.Size(69, 20);
            this.lbJH.TabIndex = 24;
            this.lbJH.Text = "机号：";
            // 
            // btnQuit
            // 
            this.btnQuit.Font = new System.Drawing.Font("宋体", 15F);
            this.btnQuit.ForeColor = System.Drawing.SystemColors.Window;
            this.btnQuit.Location = new System.Drawing.Point(693, 421);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(70, 40);
            this.btnQuit.TabIndex = 26;
            this.btnQuit.Text = "退出";
            this.btnQuit.UseVisualStyleBackColor = false;
            this.btnQuit.Visible = false;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnReboot
            // 
            this.btnReboot.Font = new System.Drawing.Font("宋体", 15F);
            this.btnReboot.ForeColor = System.Drawing.SystemColors.Window;
            this.btnReboot.Location = new System.Drawing.Point(693, 461);
            this.btnReboot.Name = "btnReboot";
            this.btnReboot.Size = new System.Drawing.Size(70, 40);
            this.btnReboot.TabIndex = 27;
            this.btnReboot.Text = "重启";
            this.btnReboot.UseVisualStyleBackColor = false;
            this.btnReboot.Visible = false;
            this.btnReboot.Click += new System.EventHandler(this.btnReboot_Click);
            // 
            // btnShutdown
            // 
            this.btnShutdown.Font = new System.Drawing.Font("宋体", 15F);
            this.btnShutdown.ForeColor = System.Drawing.SystemColors.Window;
            this.btnShutdown.Location = new System.Drawing.Point(693, 501);
            this.btnShutdown.Name = "btnShutdown";
            this.btnShutdown.Size = new System.Drawing.Size(70, 40);
            this.btnShutdown.TabIndex = 28;
            this.btnShutdown.Text = "关机";
            this.btnShutdown.UseVisualStyleBackColor = false;
            this.btnShutdown.Visible = false;
            this.btnShutdown.Click += new System.EventHandler(this.btnShutdown_Click);
            // 
            // btnReceipt
            // 
            this.btnReceipt.BackColor = System.Drawing.Color.Navy;
            this.btnReceipt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReceipt.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold);
            this.btnReceipt.ForeColor = System.Drawing.SystemColors.Window;
            this.btnReceipt.Location = new System.Drawing.Point(502, 148);
            this.btnReceipt.Name = "btnReceipt";
            this.btnReceipt.Size = new System.Drawing.Size(110, 97);
            this.btnReceipt.TabIndex = 29;
            this.btnReceipt.Text = "小票补打";
            this.btnReceipt.UseVisualStyleBackColor = false;
            this.btnReceipt.Click += new System.EventHandler(this.btnReceipt_Click);
            // 
            // btnRefund
            // 
            this.btnRefund.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnRefund.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefund.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold);
            this.btnRefund.ForeColor = System.Drawing.SystemColors.Window;
            this.btnRefund.Location = new System.Drawing.Point(502, 253);
            this.btnRefund.Name = "btnRefund";
            this.btnRefund.Size = new System.Drawing.Size(110, 202);
            this.btnRefund.TabIndex = 30;
            this.btnRefund.Text = "退款";
            this.btnRefund.UseVisualStyleBackColor = false;
            this.btnRefund.Click += new System.EventHandler(this.btnRefund_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::POSApplication.Properties.Resources.quit;
            this.btnClose.Location = new System.Drawing.Point(742, 546);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnClose.Size = new System.Drawing.Size(39, 39);
            this.btnClose.TabIndex = 25;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnRefund);
            this.Controls.Add(this.btnReceipt);
            this.Controls.Add(this.btnShutdown);
            this.Controls.Add(this.btnReboot);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lbJH);
            this.Controls.Add(this.btnLogOut);
            this.Controls.Add(this.lbStaff);
            this.Controls.Add(this.btnJob);
            this.Controls.Add(this.btnSell);
            this.Controls.Add(this.lbTime);
            this.Controls.Add(this.lbWelcome);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnCash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMain";
            this.Text = "主界面";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.VisibleChanged += new System.EventHandler(this.frmMain_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCash;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Label lbWelcome;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Button btnSell;
        private System.Windows.Forms.Button btnJob;
        private System.Windows.Forms.Label lbStaff;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbJH;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnReboot;
        private System.Windows.Forms.Button btnShutdown;
        private System.Windows.Forms.Button btnReceipt;
        private System.Windows.Forms.Button btnRefund;
    }
}