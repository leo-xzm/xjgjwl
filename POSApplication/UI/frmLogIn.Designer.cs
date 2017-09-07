namespace POSApplication.UI
{
    partial class frmLogIn
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
            this.gbLogIn = new System.Windows.Forms.GroupBox();
            this.txtStaffPwd = new System.Windows.Forms.TextBox();
            this.txtStaffID = new System.Windows.Forms.TextBox();
            this.lbStaffPwd = new System.Windows.Forms.Label();
            this.lbStaffID = new System.Windows.Forms.Label();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lbWelcome = new System.Windows.Forms.Label();
            this.miniKeyboard = new POSApplication.MiniKeyboard();
            this.gbLogIn.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLogIn
            // 
            this.gbLogIn.Controls.Add(this.txtStaffPwd);
            this.gbLogIn.Controls.Add(this.txtStaffID);
            this.gbLogIn.Controls.Add(this.lbStaffPwd);
            this.gbLogIn.Controls.Add(this.lbStaffID);
            this.gbLogIn.Font = new System.Drawing.Font("宋体", 30F);
            this.gbLogIn.ForeColor = System.Drawing.SystemColors.Window;
            this.gbLogIn.Location = new System.Drawing.Point(227, 127);
            this.gbLogIn.Name = "gbLogIn";
            this.gbLogIn.Size = new System.Drawing.Size(342, 223);
            this.gbLogIn.TabIndex = 0;
            this.gbLogIn.TabStop = false;
            this.gbLogIn.Text = "登录";
            // 
            // txtStaffPwd
            // 
            this.txtStaffPwd.Font = new System.Drawing.Font("宋体", 25F);
            this.txtStaffPwd.Location = new System.Drawing.Point(125, 146);
            this.txtStaffPwd.Name = "txtStaffPwd";
            this.txtStaffPwd.PasswordChar = '*';
            this.txtStaffPwd.Size = new System.Drawing.Size(168, 46);
            this.txtStaffPwd.TabIndex = 3;
            this.txtStaffPwd.Click += new System.EventHandler(this.txtStaffPwd_Click);
            // 
            // txtStaffID
            // 
            this.txtStaffID.Font = new System.Drawing.Font("宋体", 25F);
            this.txtStaffID.Location = new System.Drawing.Point(125, 64);
            this.txtStaffID.Name = "txtStaffID";
            this.txtStaffID.Size = new System.Drawing.Size(168, 46);
            this.txtStaffID.TabIndex = 2;
            this.txtStaffID.Click += new System.EventHandler(this.txtStaffID_Click);
            // 
            // lbStaffPwd
            // 
            this.lbStaffPwd.AutoSize = true;
            this.lbStaffPwd.Font = new System.Drawing.Font("宋体", 25F);
            this.lbStaffPwd.Location = new System.Drawing.Point(40, 149);
            this.lbStaffPwd.Name = "lbStaffPwd";
            this.lbStaffPwd.Size = new System.Drawing.Size(83, 34);
            this.lbStaffPwd.TabIndex = 1;
            this.lbStaffPwd.Text = "密码";
            // 
            // lbStaffID
            // 
            this.lbStaffID.AutoSize = true;
            this.lbStaffID.Font = new System.Drawing.Font("宋体", 25F);
            this.lbStaffID.Location = new System.Drawing.Point(40, 67);
            this.lbStaffID.Name = "lbStaffID";
            this.lbStaffID.Size = new System.Drawing.Size(83, 34);
            this.lbStaffID.TabIndex = 0;
            this.lbStaffID.Text = "工号";
            // 
            // btnLogIn
            // 
            this.btnLogIn.BackColor = System.Drawing.Color.AliceBlue;
            this.btnLogIn.FlatAppearance.BorderSize = 3;
            this.btnLogIn.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold);
            this.btnLogIn.Location = new System.Drawing.Point(322, 375);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(168, 63);
            this.btnLogIn.TabIndex = 4;
            this.btnLogIn.Text = "登 录";
            this.btnLogIn.UseVisualStyleBackColor = false;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClear.FlatAppearance.BorderSize = 3;
            this.btnClear.Font = new System.Drawing.Font("宋体", 12F);
            this.btnClear.Location = new System.Drawing.Point(574, 396);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(87, 41);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "清空重填";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lbWelcome
            // 
            this.lbWelcome.AutoSize = true;
            this.lbWelcome.Font = new System.Drawing.Font("宋体", 35F, System.Drawing.FontStyle.Bold);
            this.lbWelcome.ForeColor = System.Drawing.SystemColors.Window;
            this.lbWelcome.Location = new System.Drawing.Point(27, 34);
            this.lbWelcome.Name = "lbWelcome";
            this.lbWelcome.Size = new System.Drawing.Size(189, 47);
            this.lbWelcome.TabIndex = 7;
            this.lbWelcome.Text = "便捷通v";
            // 
            // miniKeyboard
            // 
            this.miniKeyboard.BackColor = System.Drawing.SystemColors.Window;
            this.miniKeyboard.Backspace = "退格";
            this.miniKeyboard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.miniKeyboard.Cancel = "取消";
            this.miniKeyboard.Dot = ".";
            this.miniKeyboard.KeyEnter = "确定";
            this.miniKeyboard.Location = new System.Drawing.Point(72, 356);
            this.miniKeyboard.MaximumSize = new System.Drawing.Size(309, 271);
            this.miniKeyboard.MinimumSize = new System.Drawing.Size(309, 271);
            this.miniKeyboard.Name = "miniKeyboard";
            this.miniKeyboard.Size = new System.Drawing.Size(309, 271);
            this.miniKeyboard.TabIndex = 6;
            this.miniKeyboard.Visible = false;
            this.miniKeyboard.X = "×";
            // 
            // frmLogIn
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.lbWelcome);
            this.Controls.Add(this.miniKeyboard);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnLogIn);
            this.Controls.Add(this.gbLogIn);
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogIn";
            this.Text = "登录界面";
            this.Load += new System.EventHandler(this.frmLogIn_Load);
            this.Click += new System.EventHandler(this.frmLogIn_Click);
            this.gbLogIn.ResumeLayout(false);
            this.gbLogIn.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLogIn;
        private System.Windows.Forms.TextBox txtStaffPwd;
        private System.Windows.Forms.TextBox txtStaffID;
        private System.Windows.Forms.Label lbStaffPwd;
        private System.Windows.Forms.Label lbStaffID;
        private System.Windows.Forms.Button btnLogIn;
        private System.Windows.Forms.Button btnClear;
        private MiniKeyboard miniKeyboard;
        private System.Windows.Forms.Label lbWelcome;


    }
}

