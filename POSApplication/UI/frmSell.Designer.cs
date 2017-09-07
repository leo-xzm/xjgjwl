namespace POSApplication.UI
{
    partial class frmSell
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvGoods = new System.Windows.Forms.DataGridView();
            this.spbh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GoodsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JYJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JYSL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbGoodsCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbMoneyTotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnPay = new System.Windows.Forms.Button();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.lbCard = new System.Windows.Forms.Label();
            this.btnAlter = new System.Windows.Forms.Button();
            this.btnDiscount = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSuspend = new System.Windows.Forms.Button();
            this.btnDiscountOverall = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbScanType = new System.Windows.Forms.ComboBox();
            this.miniKeyboard = new POSApplication.MiniKeyboard();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoods)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvGoods
            // 
            this.dgvGoods.AllowUserToAddRows = false;
            this.dgvGoods.AllowUserToDeleteRows = false;
            this.dgvGoods.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dgvGoods.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvGoods.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvGoods.ColumnHeadersHeight = 24;
            this.dgvGoods.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.spbh,
            this.GoodsName,
            this.JYJ,
            this.XJ,
            this.JYSL,
            this.Discount});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGoods.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGoods.Location = new System.Drawing.Point(11, 57);
            this.dgvGoods.MultiSelect = false;
            this.dgvGoods.Name = "dgvGoods";
            this.dgvGoods.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGoods.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvGoods.RowHeadersVisible = false;
            this.dgvGoods.RowTemplate.Height = 35;
            this.dgvGoods.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGoods.Size = new System.Drawing.Size(461, 404);
            this.dgvGoods.TabIndex = 1;
            this.dgvGoods.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGoods_CellClick);
            this.dgvGoods.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvGoods_MouseDown);
            this.dgvGoods.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgvGoods_MouseMove);
            // 
            // spbh
            // 
            this.spbh.DataPropertyName = "spbh";
            this.spbh.HeaderText = "货号";
            this.spbh.Name = "spbh";
            this.spbh.ReadOnly = true;
            this.spbh.Width = 70;
            // 
            // GoodsName
            // 
            this.GoodsName.DataPropertyName = "Name";
            this.GoodsName.HeaderText = "商品名称";
            this.GoodsName.Name = "GoodsName";
            this.GoodsName.ReadOnly = true;
            this.GoodsName.Width = 180;
            // 
            // JYJ
            // 
            this.JYJ.DataPropertyName = "JYJ";
            this.JYJ.HeaderText = "单价";
            this.JYJ.Name = "JYJ";
            this.JYJ.ReadOnly = true;
            this.JYJ.Width = 90;
            // 
            // XJ
            // 
            this.XJ.DataPropertyName = "XJ";
            this.XJ.HeaderText = "小计";
            this.XJ.Name = "XJ";
            this.XJ.ReadOnly = true;
            this.XJ.Width = 115;
            // 
            // JYSL
            // 
            this.JYSL.DataPropertyName = "JYSL";
            this.JYSL.HeaderText = "数量";
            this.JYSL.Name = "JYSL";
            this.JYSL.ReadOnly = true;
            this.JYSL.Width = 40;
            // 
            // Discount
            // 
            this.Discount.DataPropertyName = "Discount";
            this.Discount.HeaderText = "打折";
            this.Discount.Name = "Discount";
            this.Discount.ReadOnly = true;
            this.Discount.Width = 65;
            // 
            // lbCode
            // 
            this.lbCode.AutoSize = true;
            this.lbCode.Font = new System.Drawing.Font("宋体", 20F);
            this.lbCode.Location = new System.Drawing.Point(6, 15);
            this.lbCode.Name = "lbCode";
            this.lbCode.Size = new System.Drawing.Size(80, 27);
            this.lbCode.TabIndex = 2;
            this.lbCode.Text = "条码:";
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("宋体", 18F);
            this.txtCode.Location = new System.Drawing.Point(85, 10);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(260, 35);
            this.txtCode.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbGoodsCount);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lbMoneyTotal);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(479, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(311, 127);
            this.panel1.TabIndex = 5;
            // 
            // lbGoodsCount
            // 
            this.lbGoodsCount.AutoSize = true;
            this.lbGoodsCount.Font = new System.Drawing.Font("宋体", 30F);
            this.lbGoodsCount.Location = new System.Drawing.Point(173, 73);
            this.lbGoodsCount.Name = "lbGoodsCount";
            this.lbGoodsCount.Size = new System.Drawing.Size(37, 40);
            this.lbGoodsCount.TabIndex = 9;
            this.lbGoodsCount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 25F);
            this.label3.Location = new System.Drawing.Point(-1, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 34);
            this.label3.TabIndex = 8;
            this.label3.Text = "商品数量:";
            // 
            // lbMoneyTotal
            // 
            this.lbMoneyTotal.AutoSize = true;
            this.lbMoneyTotal.Font = new System.Drawing.Font("宋体", 35F);
            this.lbMoneyTotal.Location = new System.Drawing.Point(98, 16);
            this.lbMoneyTotal.Name = "lbMoneyTotal";
            this.lbMoneyTotal.Size = new System.Drawing.Size(92, 47);
            this.lbMoneyTotal.TabIndex = 7;
            this.lbMoneyTotal.Text = "0.0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 30F);
            this.label1.Location = new System.Drawing.Point(-1, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 40);
            this.label1.TabIndex = 6;
            this.label1.Text = "共:￥";
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReturn.Font = new System.Drawing.Font("宋体", 11F);
            this.btnReturn.Location = new System.Drawing.Point(697, 11);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(93, 33);
            this.btnReturn.TabIndex = 6;
            this.btnReturn.Text = "返回主界面";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnPay
            // 
            this.btnPay.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnPay.Font = new System.Drawing.Font("宋体", 37F, System.Drawing.FontStyle.Bold);
            this.btnPay.Location = new System.Drawing.Point(616, 472);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(174, 86);
            this.btnPay.TabIndex = 7;
            this.btnPay.Text = "结 账";
            this.btnPay.UseVisualStyleBackColor = false;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // txtCardID
            // 
            this.txtCardID.Font = new System.Drawing.Font("宋体", 18F);
            this.txtCardID.Location = new System.Drawing.Point(452, 10);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(238, 35);
            this.txtCardID.TabIndex = 9;
            this.txtCardID.Visible = false;
            this.txtCardID.VisibleChanged += new System.EventHandler(this.txtCardID_VisibleChanged);
            // 
            // lbCard
            // 
            this.lbCard.AutoSize = true;
            this.lbCard.Font = new System.Drawing.Font("宋体", 15F);
            this.lbCard.Location = new System.Drawing.Point(361, 15);
            this.lbCard.Name = "lbCard";
            this.lbCard.Size = new System.Drawing.Size(219, 20);
            this.lbCard.TabIndex = 8;
            this.lbCard.Text = "持卡人：xxx 余额：0.0";
            this.lbCard.Visible = false;
            // 
            // btnAlter
            // 
            this.btnAlter.BackColor = System.Drawing.Color.AliceBlue;
            this.btnAlter.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.btnAlter.Location = new System.Drawing.Point(7, 7);
            this.btnAlter.Name = "btnAlter";
            this.btnAlter.Size = new System.Drawing.Size(70, 70);
            this.btnAlter.TabIndex = 10;
            this.btnAlter.Text = "修改数量";
            this.btnAlter.UseVisualStyleBackColor = false;
            this.btnAlter.Click += new System.EventHandler(this.btnAlter_Click);
            // 
            // btnDiscount
            // 
            this.btnDiscount.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDiscount.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.btnDiscount.Location = new System.Drawing.Point(159, 7);
            this.btnDiscount.Name = "btnDiscount";
            this.btnDiscount.Size = new System.Drawing.Size(70, 70);
            this.btnDiscount.TabIndex = 11;
            this.btnDiscount.Text = "打折";
            this.btnDiscount.UseVisualStyleBackColor = false;
            this.btnDiscount.Click += new System.EventHandler(this.btnDiscount_Click);
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDel.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.btnDel.Location = new System.Drawing.Point(83, 7);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(70, 70);
            this.btnDel.TabIndex = 12;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.btnSuspend);
            this.panel2.Controls.Add(this.btnDiscountOverall);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnDel);
            this.panel2.Controls.Add(this.btnAlter);
            this.panel2.Controls.Add(this.btnDiscount);
            this.panel2.Location = new System.Drawing.Point(11, 472);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(462, 86);
            this.panel2.TabIndex = 13;
            // 
            // btnSuspend
            // 
            this.btnSuspend.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSuspend.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSuspend.Location = new System.Drawing.Point(387, 7);
            this.btnSuspend.Name = "btnSuspend";
            this.btnSuspend.Size = new System.Drawing.Size(70, 70);
            this.btnSuspend.TabIndex = 19;
            this.btnSuspend.Text = "挂单";
            this.btnSuspend.UseVisualStyleBackColor = false;
            this.btnSuspend.Click += new System.EventHandler(this.btnSuspend_Click);
            // 
            // btnDiscountOverall
            // 
            this.btnDiscountOverall.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDiscountOverall.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.btnDiscountOverall.Location = new System.Drawing.Point(235, 7);
            this.btnDiscountOverall.Name = "btnDiscountOverall";
            this.btnDiscountOverall.Size = new System.Drawing.Size(70, 70);
            this.btnDiscountOverall.TabIndex = 13;
            this.btnDiscountOverall.Text = "整单打折";
            this.btnDiscountOverall.UseVisualStyleBackColor = false;
            this.btnDiscountOverall.Click += new System.EventHandler(this.btnDiscountOverall_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(311, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 70);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "整单取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 18F);
            this.label2.Location = new System.Drawing.Point(482, 479);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 24);
            this.label2.TabIndex = 16;
            this.label2.Text = "扫描方式：";
            // 
            // cbbScanType
            // 
            this.cbbScanType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbScanType.Font = new System.Drawing.Font("宋体", 20F);
            this.cbbScanType.FormattingEnabled = true;
            this.cbbScanType.Location = new System.Drawing.Point(486, 523);
            this.cbbScanType.Name = "cbbScanType";
            this.cbbScanType.Size = new System.Drawing.Size(110, 35);
            this.cbbScanType.TabIndex = 17;
            this.cbbScanType.SelectedIndexChanged += new System.EventHandler(this.cbbScanType_SelectedIndexChanged);
            // 
            // miniKeyboard
            // 
            this.miniKeyboard.BackColor = System.Drawing.SystemColors.Window;
            this.miniKeyboard.Backspace = "退格";
            this.miniKeyboard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.miniKeyboard.Cancel = "取消";
            this.miniKeyboard.Dot = ".";
            this.miniKeyboard.KeyEnter = "确定";
            this.miniKeyboard.Location = new System.Drawing.Point(479, 190);
            this.miniKeyboard.Name = "miniKeyboard";
            this.miniKeyboard.Size = new System.Drawing.Size(309, 271);
            this.miniKeyboard.TabIndex = 0;
            this.miniKeyboard.X = "×";
            // 
            // frmSell
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.cbbScanType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbCard);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtCardID);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lbCode);
            this.Controls.Add(this.dgvGoods);
            this.Controls.Add(this.miniKeyboard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSell";
            this.Text = "销售收银界面";
            this.Load += new System.EventHandler(this.frmSell_Load);
            this.VisibleChanged += new System.EventHandler(this.frmSell_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoods)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MiniKeyboard miniKeyboard;
        private System.Windows.Forms.DataGridView dgvGoods;
        private System.Windows.Forms.Label lbCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbGoodsCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbMoneyTotal;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.TextBox txtCardID;
        private System.Windows.Forms.Label lbCard;
        private System.Windows.Forms.Button btnAlter;
        private System.Windows.Forms.Button btnDiscount;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnDiscountOverall;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbScanType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSuspend;
        private System.Windows.Forms.DataGridViewTextBoxColumn spbh;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoodsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn JYJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn XJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn JYSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
    }
}