namespace POSApplication.UI
{
    partial class frmConfig
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
            this.lbStaff = new System.Windows.Forms.Label();
            this.lbWelcome = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbSMQ = new System.Windows.Forms.Label();
            this.cbbCom = new System.Windows.Forms.ComboBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMDH = new System.Windows.Forms.TextBox();
            this.lbMDH = new System.Windows.Forms.Label();
            this.txtJH = new System.Windows.Forms.TextBox();
            this.lbJH = new System.Windows.Forms.Label();
            this.lbKXP = new System.Windows.Forms.Label();
            this.cbKCom = new System.Windows.Forms.ComboBox();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnClearTempOrder = new System.Windows.Forms.Button();
            this.btnBoot = new System.Windows.Forms.Button();
            this.btnUnBoot = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbStaff
            // 
            this.lbStaff.AutoSize = true;
            this.lbStaff.Font = new System.Drawing.Font("宋体", 15F);
            this.lbStaff.ForeColor = System.Drawing.SystemColors.Window;
            this.lbStaff.Location = new System.Drawing.Point(546, 51);
            this.lbStaff.Name = "lbStaff";
            this.lbStaff.Size = new System.Drawing.Size(129, 20);
            this.lbStaff.TabIndex = 9;
            this.lbStaff.Text = "员工：凌凌漆";
            // 
            // lbWelcome
            // 
            this.lbWelcome.AutoSize = true;
            this.lbWelcome.Font = new System.Drawing.Font("宋体", 35F, System.Drawing.FontStyle.Bold);
            this.lbWelcome.ForeColor = System.Drawing.SystemColors.Window;
            this.lbWelcome.Location = new System.Drawing.Point(27, 34);
            this.lbWelcome.Name = "lbWelcome";
            this.lbWelcome.Size = new System.Drawing.Size(287, 47);
            this.lbWelcome.TabIndex = 10;
            this.lbWelcome.Text = "POS操作系统";
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("宋体", 15F);
            this.lbTime.ForeColor = System.Drawing.SystemColors.Window;
            this.lbTime.Location = new System.Drawing.Point(449, 558);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(269, 20);
            this.lbTime.TabIndex = 11;
            this.lbTime.Text = "2014年12月31日12点31分00秒";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbSMQ
            // 
            this.lbSMQ.AutoSize = true;
            this.lbSMQ.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lbSMQ.ForeColor = System.Drawing.SystemColors.Window;
            this.lbSMQ.Location = new System.Drawing.Point(60, 189);
            this.lbSMQ.Name = "lbSMQ";
            this.lbSMQ.Size = new System.Drawing.Size(123, 24);
            this.lbSMQ.TabIndex = 13;
            this.lbSMQ.Text = "扫描端口:";
            // 
            // cbbCom
            // 
            this.cbbCom.Font = new System.Drawing.Font("宋体", 18F);
            this.cbbCom.FormattingEnabled = true;
            this.cbbCom.Location = new System.Drawing.Point(225, 186);
            this.cbbCom.Name = "cbbCom";
            this.cbbCom.Size = new System.Drawing.Size(121, 32);
            this.cbbCom.TabIndex = 14;
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.SteelBlue;
            this.btnReturn.Font = new System.Drawing.Font("宋体", 15F);
            this.btnReturn.ForeColor = System.Drawing.SystemColors.Window;
            this.btnReturn.Location = new System.Drawing.Point(710, 41);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(70, 40);
            this.btnReturn.TabIndex = 15;
            this.btnReturn.Text = "返回";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMDH);
            this.groupBox1.Controls.Add(this.lbMDH);
            this.groupBox1.Controls.Add(this.txtJH);
            this.groupBox1.Controls.Add(this.lbJH);
            this.groupBox1.Controls.Add(this.lbKXP);
            this.groupBox1.Controls.Add(this.cbKCom);
            this.groupBox1.Controls.Add(this.btnEnter);
            this.groupBox1.Controls.Add(this.lbSMQ);
            this.groupBox1.Controls.Add(this.cbbCom);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 22F);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.groupBox1.Location = new System.Drawing.Point(44, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(419, 391);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "系统管理";
            // 
            // txtMDH
            // 
            this.txtMDH.Font = new System.Drawing.Font("宋体", 18F);
            this.txtMDH.Location = new System.Drawing.Point(225, 57);
            this.txtMDH.Name = "txtMDH";
            this.txtMDH.Size = new System.Drawing.Size(121, 35);
            this.txtMDH.TabIndex = 23;
            // 
            // lbMDH
            // 
            this.lbMDH.AutoSize = true;
            this.lbMDH.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lbMDH.ForeColor = System.Drawing.SystemColors.Window;
            this.lbMDH.Location = new System.Drawing.Point(85, 60);
            this.lbMDH.Name = "lbMDH";
            this.lbMDH.Size = new System.Drawing.Size(98, 24);
            this.lbMDH.TabIndex = 22;
            this.lbMDH.Text = "门店号:";
            // 
            // txtJH
            // 
            this.txtJH.Font = new System.Drawing.Font("宋体", 18F);
            this.txtJH.Location = new System.Drawing.Point(225, 122);
            this.txtJH.Name = "txtJH";
            this.txtJH.Size = new System.Drawing.Size(121, 35);
            this.txtJH.TabIndex = 21;
            // 
            // lbJH
            // 
            this.lbJH.AutoSize = true;
            this.lbJH.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lbJH.ForeColor = System.Drawing.SystemColors.Window;
            this.lbJH.Location = new System.Drawing.Point(58, 125);
            this.lbJH.Name = "lbJH";
            this.lbJH.Size = new System.Drawing.Size(125, 24);
            this.lbJH.TabIndex = 20;
            this.lbJH.Text = "POS 机号:";
            // 
            // lbKXP
            // 
            this.lbKXP.AutoSize = true;
            this.lbKXP.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lbKXP.ForeColor = System.Drawing.SystemColors.Window;
            this.lbKXP.Location = new System.Drawing.Point(35, 254);
            this.lbKXP.Name = "lbKXP";
            this.lbKXP.Size = new System.Drawing.Size(148, 24);
            this.lbKXP.TabIndex = 18;
            this.lbKXP.Text = "客显牌端口:";
            // 
            // cbKCom
            // 
            this.cbKCom.Font = new System.Drawing.Font("宋体", 18F);
            this.cbKCom.FormattingEnabled = true;
            this.cbKCom.Location = new System.Drawing.Point(225, 251);
            this.cbKCom.Name = "cbKCom";
            this.cbKCom.Size = new System.Drawing.Size(121, 32);
            this.cbKCom.TabIndex = 19;
            // 
            // btnEnter
            // 
            this.btnEnter.BackColor = System.Drawing.Color.AliceBlue;
            this.btnEnter.Font = new System.Drawing.Font("宋体", 14F);
            this.btnEnter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnEnter.Location = new System.Drawing.Point(225, 312);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(121, 40);
            this.btnEnter.TabIndex = 17;
            this.btnEnter.Text = "确  认";
            this.btnEnter.UseVisualStyleBackColor = false;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnClearTempOrder
            // 
            this.btnClearTempOrder.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClearTempOrder.Font = new System.Drawing.Font("宋体", 14F);
            this.btnClearTempOrder.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClearTempOrder.Location = new System.Drawing.Point(554, 319);
            this.btnClearTempOrder.Name = "btnClearTempOrder";
            this.btnClearTempOrder.Size = new System.Drawing.Size(121, 40);
            this.btnClearTempOrder.TabIndex = 22;
            this.btnClearTempOrder.Text = "清除缓存";
            this.btnClearTempOrder.UseVisualStyleBackColor = false;
            this.btnClearTempOrder.Click += new System.EventHandler(this.btnClearTempOrder_Click);
            // 
            // btnBoot
            // 
            this.btnBoot.BackColor = System.Drawing.Color.AliceBlue;
            this.btnBoot.Font = new System.Drawing.Font("宋体", 14F);
            this.btnBoot.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBoot.Location = new System.Drawing.Point(554, 384);
            this.btnBoot.Name = "btnBoot";
            this.btnBoot.Size = new System.Drawing.Size(121, 40);
            this.btnBoot.TabIndex = 23;
            this.btnBoot.Text = "开机启动";
            this.btnBoot.UseVisualStyleBackColor = false;
            this.btnBoot.Visible = false;
            this.btnBoot.Click += new System.EventHandler(this.btnBoot_Click);
            // 
            // btnUnBoot
            // 
            this.btnUnBoot.BackColor = System.Drawing.Color.AliceBlue;
            this.btnUnBoot.Font = new System.Drawing.Font("宋体", 14F);
            this.btnUnBoot.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnUnBoot.Location = new System.Drawing.Point(554, 448);
            this.btnUnBoot.Name = "btnUnBoot";
            this.btnUnBoot.Size = new System.Drawing.Size(142, 40);
            this.btnUnBoot.TabIndex = 24;
            this.btnUnBoot.Text = "取消开机启动";
            this.btnUnBoot.UseVisualStyleBackColor = false;
            this.btnUnBoot.Visible = false;
            this.btnUnBoot.Click += new System.EventHandler(this.btnUnBoot_Click);
            // 
            // frmConfig
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnUnBoot);
            this.Controls.Add(this.btnBoot);
            this.Controls.Add(this.btnClearTempOrder);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.lbTime);
            this.Controls.Add(this.lbStaff);
            this.Controls.Add(this.lbWelcome);
            this.Font = new System.Drawing.Font("宋体", 18F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConfig";
            this.Text = "系统管理界面";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbStaff;
        private System.Windows.Forms.Label lbWelcome;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbSMQ;
        private System.Windows.Forms.ComboBox cbbCom;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Label lbKXP;
        private System.Windows.Forms.ComboBox cbKCom;
        private System.Windows.Forms.TextBox txtJH;
        private System.Windows.Forms.Label lbJH;
        private System.Windows.Forms.Button btnClearTempOrder;
        private System.Windows.Forms.Button btnBoot;
        private System.Windows.Forms.TextBox txtMDH;
        private System.Windows.Forms.Label lbMDH;
        private System.Windows.Forms.Button btnUnBoot;
    }
}