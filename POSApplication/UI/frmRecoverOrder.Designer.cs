namespace POSApplication.UI
{
    partial class frmRecoverOrder
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
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvGoods = new System.Windows.Forms.DataGridView();
            this.spbh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GoodsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JYJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JYSL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbbSuspendList = new System.Windows.Forms.ComboBox();
            this.lbOrder = new System.Windows.Forms.Label();
            this.btnRecover = new System.Windows.Forms.Button();
            this.lbJH = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoods)).BeginInit();
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
            this.groupBox1.Controls.Add(this.btnDeleteAll);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.dgvGoods);
            this.groupBox1.Controls.Add(this.cbbSuspendList);
            this.groupBox1.Controls.Add(this.lbOrder);
            this.groupBox1.Controls.Add(this.btnRecover);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 22F);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.groupBox1.Location = new System.Drawing.Point(44, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(712, 450);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "挂单管理";
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDeleteAll.Font = new System.Drawing.Font("宋体", 15F);
            this.btnDeleteAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDeleteAll.Location = new System.Drawing.Point(403, 42);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(60, 60);
            this.btnDeleteAll.TabIndex = 26;
            this.btnDeleteAll.Text = "清空列表";
            this.btnDeleteAll.UseVisualStyleBackColor = false;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDelete.Enabled = false;
            this.btnDelete.Font = new System.Drawing.Font("宋体", 15F);
            this.btnDelete.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDelete.Location = new System.Drawing.Point(484, 42);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 60);
            this.btnDelete.TabIndex = 25;
            this.btnDelete.Text = "删除挂单";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgvGoods
            // 
            this.dgvGoods.AllowUserToAddRows = false;
            this.dgvGoods.AllowUserToDeleteRows = false;
            this.dgvGoods.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dgvGoods.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvGoods.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGoods.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGoods.ColumnHeadersHeight = 24;
            this.dgvGoods.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.spbh,
            this.GoodsName,
            this.JYJ,
            this.JYSL,
            this.XJ,
            this.Discount});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 22F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGoods.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvGoods.Location = new System.Drawing.Point(26, 118);
            this.dgvGoods.MultiSelect = false;
            this.dgvGoods.Name = "dgvGoods";
            this.dgvGoods.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 22F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGoods.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvGoods.RowHeadersVisible = false;
            this.dgvGoods.RowTemplate.Height = 35;
            this.dgvGoods.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGoods.Size = new System.Drawing.Size(663, 308);
            this.dgvGoods.TabIndex = 24;
            this.dgvGoods.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvGoods_MouseDown);
            this.dgvGoods.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgvGoods_MouseMove);
            // 
            // spbh
            // 
            this.spbh.DataPropertyName = "spbh";
            this.spbh.HeaderText = "货号";
            this.spbh.Name = "spbh";
            this.spbh.ReadOnly = true;
            // 
            // GoodsName
            // 
            this.GoodsName.DataPropertyName = "Name";
            this.GoodsName.HeaderText = "商品名称";
            this.GoodsName.Name = "GoodsName";
            this.GoodsName.ReadOnly = true;
            this.GoodsName.Width = 210;
            // 
            // JYJ
            // 
            this.JYJ.DataPropertyName = "JYJ";
            this.JYJ.HeaderText = "单价";
            this.JYJ.Name = "JYJ";
            this.JYJ.ReadOnly = true;
            // 
            // JYSL
            // 
            this.JYSL.DataPropertyName = "JYSL";
            this.JYSL.HeaderText = "数量";
            this.JYSL.Name = "JYSL";
            this.JYSL.ReadOnly = true;
            this.JYSL.Width = 40;
            // 
            // XJ
            // 
            this.XJ.DataPropertyName = "XJ";
            this.XJ.HeaderText = "小计";
            this.XJ.Name = "XJ";
            this.XJ.ReadOnly = true;
            this.XJ.Width = 115;
            // 
            // Discount
            // 
            this.Discount.DataPropertyName = "Discount";
            this.Discount.HeaderText = "打折";
            this.Discount.Name = "Discount";
            this.Discount.ReadOnly = true;
            this.Discount.Width = 65;
            // 
            // cbbSuspendList
            // 
            this.cbbSuspendList.Font = new System.Drawing.Font("宋体", 22F);
            this.cbbSuspendList.FormattingEnabled = true;
            this.cbbSuspendList.Location = new System.Drawing.Point(146, 55);
            this.cbbSuspendList.Name = "cbbSuspendList";
            this.cbbSuspendList.Size = new System.Drawing.Size(227, 37);
            this.cbbSuspendList.TabIndex = 23;
            this.cbbSuspendList.SelectedIndexChanged += new System.EventHandler(this.cbbSuspendList_SelectedIndexChanged);
            // 
            // lbOrder
            // 
            this.lbOrder.AutoSize = true;
            this.lbOrder.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lbOrder.ForeColor = System.Drawing.SystemColors.Window;
            this.lbOrder.Location = new System.Drawing.Point(22, 62);
            this.lbOrder.Name = "lbOrder";
            this.lbOrder.Size = new System.Drawing.Size(135, 24);
            this.lbOrder.TabIndex = 22;
            this.lbOrder.Text = "挂单列表：";
            // 
            // btnRecover
            // 
            this.btnRecover.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnRecover.Enabled = false;
            this.btnRecover.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.btnRecover.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRecover.Location = new System.Drawing.Point(562, 42);
            this.btnRecover.Name = "btnRecover";
            this.btnRecover.Size = new System.Drawing.Size(127, 60);
            this.btnRecover.TabIndex = 17;
            this.btnRecover.Text = "恢复挂单";
            this.btnRecover.UseVisualStyleBackColor = false;
            this.btnRecover.Click += new System.EventHandler(this.btnRecover_Click);
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
            // frmRecoverOrder
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
            this.Name = "frmRecoverOrder";
            this.Text = "挂单恢复界面";
            this.Load += new System.EventHandler(this.frmRecoverOrder_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoods)).EndInit();
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
        private System.Windows.Forms.Button btnRecover;
        private System.Windows.Forms.Label lbOrder;
        private System.Windows.Forms.Label lbJH;
        private System.Windows.Forms.ComboBox cbbSuspendList;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgvGoods;
        private System.Windows.Forms.DataGridViewTextBoxColumn spbh;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoodsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn JYJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn JYSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn XJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
        private System.Windows.Forms.Button btnDeleteAll;

    }
}