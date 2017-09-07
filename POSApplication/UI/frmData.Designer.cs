namespace POSApplication.UI
{
    partial class frmData
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
            this.btnReturn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpSalesDay = new System.Windows.Forms.DateTimePicker();
            this.lbDate = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.lbJH = new System.Windows.Forms.Label();
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
            this.groupBox1.Controls.Add(this.dtpSalesDay);
            this.groupBox1.Controls.Add(this.lbDate);
            this.groupBox1.Controls.Add(this.btnUpload);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 22F);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.groupBox1.Location = new System.Drawing.Point(44, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(514, 226);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据上传";
            // 
            // dtpSalesDay
            // 
            this.dtpSalesDay.Location = new System.Drawing.Point(201, 70);
            this.dtpSalesDay.Name = "dtpSalesDay";
            this.dtpSalesDay.Size = new System.Drawing.Size(200, 41);
            this.dtpSalesDay.TabIndex = 24;
            this.dtpSalesDay.Value = new System.DateTime(2015, 7, 31, 0, 0, 0, 0);
            // 
            // lbDate
            // 
            this.lbDate.AutoSize = true;
            this.lbDate.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lbDate.ForeColor = System.Drawing.SystemColors.Window;
            this.lbDate.Location = new System.Drawing.Point(60, 82);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(135, 24);
            this.lbDate.TabIndex = 22;
            this.lbDate.Text = "指定日期：";
            // 
            // btnUpload
            // 
            this.btnUpload.BackColor = System.Drawing.Color.AliceBlue;
            this.btnUpload.Font = new System.Drawing.Font("宋体", 14F);
            this.btnUpload.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnUpload.Location = new System.Drawing.Point(280, 141);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(121, 40);
            this.btnUpload.TabIndex = 17;
            this.btnUpload.Text = "上  传";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // lbJH
            // 
            this.lbJH.AutoSize = true;
            this.lbJH.Font = new System.Drawing.Font("宋体", 15F);
            this.lbJH.ForeColor = System.Drawing.SystemColors.Window;
            this.lbJH.Location = new System.Drawing.Point(420, 51);
            this.lbJH.Name = "lbJH";
            this.lbJH.Size = new System.Drawing.Size(69, 20);
            this.lbJH.TabIndex = 25;
            this.lbJH.Text = "机号：";
            // 
            // frmData
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.lbJH);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.lbTime);
            this.Controls.Add(this.lbStaff);
            this.Controls.Add(this.lbWelcome);
            this.Font = new System.Drawing.Font("宋体", 18F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmData";
            this.Text = "数据上传界面";
            this.Load += new System.EventHandler(this.frmData_Load);
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
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Label lbDate;
        private System.Windows.Forms.DateTimePicker dtpSalesDay;
        private System.Windows.Forms.Label lbJH;
    }
}
