namespace POSApplication.UI
{
    partial class frmReceipt
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbStaff = new System.Windows.Forms.Label();
            this.lbWelcome = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnReturn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtReceipt = new System.Windows.Forms.TextBox();
            this.cbbDay = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbMonth = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvReceipt = new System.Windows.Forms.DataGridView();
            this.xph = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.printTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.syybh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actualTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPrint = new System.Windows.Forms.Button();
            this.cbbYear = new System.Windows.Forms.ComboBox();
            this.lbOrder = new System.Windows.Forms.Label();
            this.lbJH = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipt)).BeginInit();
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
            this.groupBox1.Controls.Add(this.txtReceipt);
            this.groupBox1.Controls.Add(this.cbbDay);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbbMonth);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dgvReceipt);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.cbbYear);
            this.groupBox1.Controls.Add(this.lbOrder);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 22F);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.groupBox1.Location = new System.Drawing.Point(44, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(712, 450);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "小票补打";
            // 
            // txtReceipt
            // 
            this.txtReceipt.Font = new System.Drawing.Font("宋体", 8F);
            this.txtReceipt.Location = new System.Drawing.Point(476, 96);
            this.txtReceipt.Multiline = true;
            this.txtReceipt.Name = "txtReceipt";
            this.txtReceipt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtReceipt.Size = new System.Drawing.Size(215, 340);
            this.txtReceipt.TabIndex = 31;
            this.txtReceipt.WordWrap = false;
            // 
            // cbbDay
            // 
            this.cbbDay.Font = new System.Drawing.Font("宋体", 22F);
            this.cbbDay.FormattingEnabled = true;
            this.cbbDay.Location = new System.Drawing.Point(376, 43);
            this.cbbDay.Name = "cbbDay";
            this.cbbDay.Size = new System.Drawing.Size(69, 37);
            this.cbbDay.TabIndex = 30;
            this.cbbDay.SelectedIndexChanged += new System.EventHandler(this.cbbDay_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(327, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 24);
            this.label2.TabIndex = 29;
            this.label2.Text = "日：";
            // 
            // cbbMonth
            // 
            this.cbbMonth.Font = new System.Drawing.Font("宋体", 22F);
            this.cbbMonth.FormattingEnabled = true;
            this.cbbMonth.Location = new System.Drawing.Point(238, 43);
            this.cbbMonth.Name = "cbbMonth";
            this.cbbMonth.Size = new System.Drawing.Size(65, 37);
            this.cbbMonth.TabIndex = 28;
            this.cbbMonth.SelectedIndexChanged += new System.EventHandler(this.cbbMonth_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(187, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 24);
            this.label1.TabIndex = 27;
            this.label1.Text = "月：";
            // 
            // dgvReceipt
            // 
            this.dgvReceipt.AllowUserToAddRows = false;
            this.dgvReceipt.AllowUserToDeleteRows = false;
            this.dgvReceipt.AllowUserToResizeColumns = false;
            this.dgvReceipt.AllowUserToResizeRows = false;
            this.dgvReceipt.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReceipt.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dgvReceipt.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvReceipt.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 22F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReceipt.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReceipt.ColumnHeadersHeight = 24;
            this.dgvReceipt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.xph,
            this.printTime,
            this.syybh,
            this.actualTotal,
            this.mName});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 22F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReceipt.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvReceipt.Location = new System.Drawing.Point(19, 96);
            this.dgvReceipt.MultiSelect = false;
            this.dgvReceipt.Name = "dgvReceipt";
            this.dgvReceipt.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 22F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReceipt.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvReceipt.RowHeadersVisible = false;
            this.dgvReceipt.RowTemplate.Height = 35;
            this.dgvReceipt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReceipt.Size = new System.Drawing.Size(451, 340);
            this.dgvReceipt.TabIndex = 24;
            this.dgvReceipt.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReceipt_CellClick);
            this.dgvReceipt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvReceipt_MouseDown);
            this.dgvReceipt.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgvReceipt_MouseMove);
            // 
            // xph
            // 
            this.xph.DataPropertyName = "xph";
            this.xph.FillWeight = 104.9152F;
            this.xph.HeaderText = "小票号";
            this.xph.Name = "xph";
            this.xph.ReadOnly = true;
            // 
            // printTime
            // 
            this.printTime.DataPropertyName = "printTime";
            this.printTime.FillWeight = 194.2429F;
            this.printTime.HeaderText = "时间";
            this.printTime.Name = "printTime";
            this.printTime.ReadOnly = true;
            // 
            // syybh
            // 
            this.syybh.DataPropertyName = "syybh";
            this.syybh.FillWeight = 106.599F;
            this.syybh.HeaderText = "收款员";
            this.syybh.Name = "syybh";
            this.syybh.ReadOnly = true;
            // 
            // actualTotal
            // 
            this.actualTotal.DataPropertyName = "actualTotal";
            this.actualTotal.FillWeight = 97.12146F;
            this.actualTotal.HeaderText = "总计";
            this.actualTotal.Name = "actualTotal";
            this.actualTotal.ReadOnly = true;
            // 
            // mName
            // 
            this.mName.DataPropertyName = "mName";
            this.mName.FillWeight = 97.12146F;
            this.mName.HeaderText = "会员名";
            this.mName.Name = "mName";
            this.mName.ReadOnly = true;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnPrint.Enabled = false;
            this.btnPrint.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPrint.Location = new System.Drawing.Point(605, 38);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(86, 42);
            this.btnPrint.TabIndex = 17;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // cbbYear
            // 
            this.cbbYear.Font = new System.Drawing.Font("宋体", 22F);
            this.cbbYear.FormattingEnabled = true;
            this.cbbYear.Location = new System.Drawing.Point(73, 43);
            this.cbbYear.Name = "cbbYear";
            this.cbbYear.Size = new System.Drawing.Size(90, 37);
            this.cbbYear.TabIndex = 23;
            this.cbbYear.SelectedIndexChanged += new System.EventHandler(this.cbbYear_SelectedIndexChanged);
            // 
            // lbOrder
            // 
            this.lbOrder.AutoSize = true;
            this.lbOrder.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lbOrder.ForeColor = System.Drawing.SystemColors.Window;
            this.lbOrder.Location = new System.Drawing.Point(22, 50);
            this.lbOrder.Name = "lbOrder";
            this.lbOrder.Size = new System.Drawing.Size(60, 24);
            this.lbOrder.TabIndex = 22;
            this.lbOrder.Text = "年：";
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
            // frmReceipt
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(830, 610);
            this.Controls.Add(this.lbJH);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.lbTime);
            this.Controls.Add(this.lbStaff);
            this.Controls.Add(this.lbWelcome);
            this.Font = new System.Drawing.Font("宋体", 18F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReceipt";
            this.Text = "小票补打界面";
            this.Load += new System.EventHandler(this.frmReceipt_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipt)).EndInit();
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
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lbOrder;
        private System.Windows.Forms.Label lbJH;
        private System.Windows.Forms.ComboBox cbbYear;
        private System.Windows.Forms.DataGridView dgvReceipt;
        private System.Windows.Forms.ComboBox cbbDay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbMonth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReceipt;
        private System.Windows.Forms.DataGridViewTextBoxColumn xph;
        private System.Windows.Forms.DataGridViewTextBoxColumn printTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn syybh;
        private System.Windows.Forms.DataGridViewTextBoxColumn actualTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn mName;
    }
}