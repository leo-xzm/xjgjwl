namespace POSApplication.UserController
{
    partial class BottomBar
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lbStaff = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbJH = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbStaff
            // 
            this.lbStaff.AutoSize = true;
            this.lbStaff.Font = new System.Drawing.Font("宋体", 11F);
            this.lbStaff.Location = new System.Drawing.Point(458, 5);
            this.lbStaff.Name = "lbStaff";
            this.lbStaff.Size = new System.Drawing.Size(52, 15);
            this.lbStaff.TabIndex = 0;
            this.lbStaff.Text = "员工：";
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("宋体", 11F);
            this.lbTime.Location = new System.Drawing.Point(590, 5);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(209, 15);
            this.lbTime.TabIndex = 1;
            this.lbTime.Text = "2014年12月31日12点31分00秒";
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.Font = new System.Drawing.Font("宋体", 10F);
            this.lbVersion.Location = new System.Drawing.Point(18, 5);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(84, 14);
            this.lbVersion.TabIndex = 2;
            this.lbVersion.Text = "POS操作系统";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbJH
            // 
            this.lbJH.AutoSize = true;
            this.lbJH.Font = new System.Drawing.Font("宋体", 11F);
            this.lbJH.Location = new System.Drawing.Point(329, 5);
            this.lbJH.Name = "lbJH";
            this.lbJH.Size = new System.Drawing.Size(52, 15);
            this.lbJH.TabIndex = 3;
            this.lbJH.Text = "机号：";
            // 
            // BottomBar
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.lbJH);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.lbTime);
            this.Controls.Add(this.lbStaff);
            this.Name = "BottomBar";
            this.Size = new System.Drawing.Size(800, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbStaff;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbJH;
    }
}
