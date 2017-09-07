namespace POSApplication.UI
{
    partial class frmRef
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvGoods = new System.Windows.Forms.DataGridView();
            this.spbh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GoodsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JYJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JYSL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbGoodsCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbMoneyTotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.miniKeyboard = new POSApplication.MiniKeyboard();
            this.btnPay = new System.Windows.Forms.Button();
            this.lbCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbScanType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoods)).BeginInit();
            this.panel1.SuspendLayout();
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
            this.JYSL,
            this.XJ,
            this.Discount});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("宋体", 20F);
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGoods.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvGoods.Location = new System.Drawing.Point(1, 97);
            this.dgvGoods.MultiSelect = false;
            this.dgvGoods.Name = "dgvGoods";
            this.dgvGoods.ReadOnly = true;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGoods.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvGoods.RowHeadersVisible = false;
            this.dgvGoods.RowTemplate.Height = 35;
            this.dgvGoods.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGoods.Size = new System.Drawing.Size(461, 404);
            this.dgvGoods.TabIndex = 2;
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbGoodsCount);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lbMoneyTotal);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(489, 97);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(311, 127);
            this.panel1.TabIndex = 6;
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
            // miniKeyboard
            // 
            this.miniKeyboard.BackColor = System.Drawing.SystemColors.Window;
            this.miniKeyboard.Backspace = "退格";
            this.miniKeyboard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.miniKeyboard.Cancel = "取消";
            this.miniKeyboard.Dot = ".";
            this.miniKeyboard.KeyEnter = "确定";
            this.miniKeyboard.Location = new System.Drawing.Point(489, 230);
            this.miniKeyboard.Name = "miniKeyboard";
            this.miniKeyboard.Size = new System.Drawing.Size(309, 271);
            this.miniKeyboard.TabIndex = 7;
            this.miniKeyboard.X = "×";
            // 
            // btnPay
            // 
            this.btnPay.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnPay.Font = new System.Drawing.Font("宋体", 37F, System.Drawing.FontStyle.Bold);
            this.btnPay.Location = new System.Drawing.Point(560, 538);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(174, 86);
            this.btnPay.TabIndex = 8;
            this.btnPay.Text = "结 账";
            this.btnPay.UseVisualStyleBackColor = false;
            // 
            // lbCode
            // 
            this.lbCode.AutoSize = true;
            this.lbCode.Font = new System.Drawing.Font("宋体", 20F);
            this.lbCode.Location = new System.Drawing.Point(12, 31);
            this.lbCode.Name = "lbCode";
            this.lbCode.Size = new System.Drawing.Size(80, 27);
            this.lbCode.TabIndex = 9;
            this.lbCode.Text = "条码:";
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("宋体", 18F);
            this.txtCode.Location = new System.Drawing.Point(98, 30);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(260, 35);
            this.txtCode.TabIndex = 10;
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReturn.Font = new System.Drawing.Font("宋体", 11F);
            this.btnReturn.Location = new System.Drawing.Point(656, 25);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(93, 33);
            this.btnReturn.TabIndex = 11;
            this.btnReturn.Text = "返回主界面";
            this.btnReturn.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 18F);
            this.label2.Location = new System.Drawing.Point(385, 538);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 24);
            this.label2.TabIndex = 17;
            this.label2.Text = "扫描方式：";
            // 
            // cbbScanType
            // 
            this.cbbScanType.Font = new System.Drawing.Font("宋体", 20F);
            this.cbbScanType.FormattingEnabled = true;
            this.cbbScanType.Location = new System.Drawing.Point(389, 573);
            this.cbbScanType.Name = "cbbScanType";
            this.cbbScanType.Size = new System.Drawing.Size(110, 35);
            this.cbbScanType.TabIndex = 18;
            // 
            // frmRef
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 646);
            this.ControlBox = false;
            this.Controls.Add(this.cbbScanType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lbCode);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.miniKeyboard);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvGoods);
            this.Name = "frmRef";
            this.Load += new System.EventHandler(this.frmRef_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoods)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGoods;
        private System.Windows.Forms.DataGridViewTextBoxColumn spbh;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoodsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn JYJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn JYSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn XJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbGoodsCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbMoneyTotal;
        private System.Windows.Forms.Label label1;
        private MiniKeyboard miniKeyboard;
        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.Label lbCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbScanType;
    }
}