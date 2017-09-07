namespace POSApplication.UI
{
    partial class frmPay
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbCash = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbChange = new System.Windows.Forms.Label();
            this.txtPayCash = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lbCardPay = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbYH = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbGoodsCount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbMoneyTotal = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvCardPay = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnAlter = new System.Windows.Forms.Button();
            this.btnPay = new System.Windows.Forms.Button();
            this.btnMCardPay = new System.Windows.Forms.Button();
            this.miniKeyboard = new POSApplication.MiniKeyboard();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCardPay)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 27F);
            this.label1.Location = new System.Drawing.Point(10, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 36);
            this.label1.TabIndex = 2;
            this.label1.Text = "应收现金:￥";
            // 
            // lbCash
            // 
            this.lbCash.AutoSize = true;
            this.lbCash.Font = new System.Drawing.Font("宋体", 40F);
            this.lbCash.Location = new System.Drawing.Point(220, 69);
            this.lbCash.Name = "lbCash";
            this.lbCash.Size = new System.Drawing.Size(104, 54);
            this.lbCash.TabIndex = 3;
            this.lbCash.Text = "0.0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 27F);
            this.label3.Location = new System.Drawing.Point(10, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 36);
            this.label3.TabIndex = 4;
            this.label3.Text = "实收现金:￥";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 27F);
            this.label4.Location = new System.Drawing.Point(82, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 36);
            this.label4.TabIndex = 5;
            this.label4.Text = "找零:￥";
            // 
            // lbChange
            // 
            this.lbChange.AutoSize = true;
            this.lbChange.Font = new System.Drawing.Font("宋体", 40F);
            this.lbChange.Location = new System.Drawing.Point(220, 213);
            this.lbChange.Name = "lbChange";
            this.lbChange.Size = new System.Drawing.Size(104, 54);
            this.lbChange.TabIndex = 6;
            this.lbChange.Text = "0.0";
            // 
            // txtPayCash
            // 
            this.txtPayCash.Font = new System.Drawing.Font("宋体", 40F);
            this.txtPayCash.Location = new System.Drawing.Point(229, 135);
            this.txtPayCash.Name = "txtPayCash";
            this.txtPayCash.Size = new System.Drawing.Size(192, 68);
            this.txtPayCash.TabIndex = 7;
            this.txtPayCash.Text = "0.0";
            this.txtPayCash.TextChanged += new System.EventHandler(this.txtPayCash_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbCardPay);
            this.panel1.Controls.Add(this.txtPayCash);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbCash);
            this.panel1.Controls.Add(this.lbChange);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(12, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(453, 280);
            this.panel1.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 27F);
            this.label2.Location = new System.Drawing.Point(10, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 36);
            this.label2.TabIndex = 8;
            this.label2.Text = "刷卡金额:￥";
            // 
            // lbCardPay
            // 
            this.lbCardPay.AutoSize = true;
            this.lbCardPay.Font = new System.Drawing.Font("宋体", 40F);
            this.lbCardPay.Location = new System.Drawing.Point(220, 8);
            this.lbCardPay.Name = "lbCardPay";
            this.lbCardPay.Size = new System.Drawing.Size(104, 54);
            this.lbCardPay.TabIndex = 9;
            this.lbCardPay.Text = "0.0";
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReturn.Font = new System.Drawing.Font("宋体", 11F);
            this.btnReturn.Location = new System.Drawing.Point(12, 12);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(48, 33);
            this.btnReturn.TabIndex = 9;
            this.btnReturn.Text = "返回";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lbYH);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.lbGoodsCount);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.lbMoneyTotal);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(479, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(311, 172);
            this.panel2.TabIndex = 10;
            // 
            // lbYH
            // 
            this.lbYH.AutoSize = true;
            this.lbYH.Font = new System.Drawing.Font("宋体", 30F);
            this.lbYH.Location = new System.Drawing.Point(124, 16);
            this.lbYH.Name = "lbYH";
            this.lbYH.Size = new System.Drawing.Size(77, 40);
            this.lbYH.TabIndex = 11;
            this.lbYH.Text = "0.0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 25F);
            this.label8.Location = new System.Drawing.Point(-1, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(134, 34);
            this.label8.TabIndex = 10;
            this.label8.Text = "优惠:￥";
            // 
            // lbGoodsCount
            // 
            this.lbGoodsCount.AutoSize = true;
            this.lbGoodsCount.Font = new System.Drawing.Font("宋体", 25F);
            this.lbGoodsCount.Location = new System.Drawing.Point(134, 132);
            this.lbGoodsCount.Name = "lbGoodsCount";
            this.lbGoodsCount.Size = new System.Drawing.Size(32, 34);
            this.lbGoodsCount.TabIndex = 9;
            this.lbGoodsCount.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 20F);
            this.label6.Location = new System.Drawing.Point(3, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 27);
            this.label6.TabIndex = 8;
            this.label6.Text = "商品数量:";
            // 
            // lbMoneyTotal
            // 
            this.lbMoneyTotal.AutoSize = true;
            this.lbMoneyTotal.Font = new System.Drawing.Font("宋体", 40F);
            this.lbMoneyTotal.Location = new System.Drawing.Point(131, 65);
            this.lbMoneyTotal.Name = "lbMoneyTotal";
            this.lbMoneyTotal.Size = new System.Drawing.Size(104, 54);
            this.lbMoneyTotal.TabIndex = 7;
            this.lbMoneyTotal.Text = "0.0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 27F);
            this.label7.Location = new System.Drawing.Point(-1, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(141, 36);
            this.label7.TabIndex = 6;
            this.label7.Text = "总计:￥";
            // 
            // dgvCardPay
            // 
            this.dgvCardPay.AllowUserToAddRows = false;
            this.dgvCardPay.AllowUserToDeleteRows = false;
            this.dgvCardPay.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCardPay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCardPay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgvCardPay.Location = new System.Drawing.Point(12, 345);
            this.dgvCardPay.Name = "dgvCardPay";
            this.dgvCardPay.ReadOnly = true;
            this.dgvCardPay.RowHeadersVisible = false;
            this.dgvCardPay.RowTemplate.Height = 27;
            this.dgvCardPay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCardPay.Size = new System.Drawing.Size(273, 116);
            this.dgvCardPay.TabIndex = 11;
            this.dgvCardPay.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCardPay_CellClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Card";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column1.HeaderText = "卡名";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "je";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column2.HeaderText = "支付金额";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 110;
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDel.Font = new System.Drawing.Font("宋体", 21F);
            this.btnDel.Location = new System.Drawing.Point(385, 345);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(80, 55);
            this.btnDel.TabIndex = 16;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.AliceBlue;
            this.btnAdd.Font = new System.Drawing.Font("宋体", 24F);
            this.btnAdd.Location = new System.Drawing.Point(291, 345);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 116);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "增加卡付";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnAlter
            // 
            this.btnAlter.BackColor = System.Drawing.Color.AliceBlue;
            this.btnAlter.Font = new System.Drawing.Font("宋体", 21F);
            this.btnAlter.Location = new System.Drawing.Point(385, 406);
            this.btnAlter.Name = "btnAlter";
            this.btnAlter.Size = new System.Drawing.Size(80, 55);
            this.btnAlter.TabIndex = 15;
            this.btnAlter.Text = "修改";
            this.btnAlter.UseVisualStyleBackColor = false;
            this.btnAlter.Click += new System.EventHandler(this.btnAlter_Click);
            // 
            // btnPay
            // 
            this.btnPay.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnPay.Font = new System.Drawing.Font("宋体", 37F, System.Drawing.FontStyle.Bold);
            this.btnPay.Location = new System.Drawing.Point(616, 472);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(174, 86);
            this.btnPay.TabIndex = 17;
            this.btnPay.Text = "结 账";
            this.btnPay.UseVisualStyleBackColor = false;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // btnMCardPay
            // 
            this.btnMCardPay.BackColor = System.Drawing.Color.AliceBlue;
            this.btnMCardPay.Font = new System.Drawing.Font("宋体", 20F);
            this.btnMCardPay.Location = new System.Drawing.Point(291, 472);
            this.btnMCardPay.Name = "btnMCardPay";
            this.btnMCardPay.Size = new System.Drawing.Size(174, 86);
            this.btnMCardPay.TabIndex = 18;
            this.btnMCardPay.Text = "员工卡支付";
            this.btnMCardPay.UseVisualStyleBackColor = false;
            this.btnMCardPay.Visible = false;
            this.btnMCardPay.Click += new System.EventHandler(this.btnMCardPay_Click);
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
            this.miniKeyboard.MaximumSize = new System.Drawing.Size(311, 271);
            this.miniKeyboard.MinimumSize = new System.Drawing.Size(311, 271);
            this.miniKeyboard.Name = "miniKeyboard";
            this.miniKeyboard.Size = new System.Drawing.Size(311, 271);
            this.miniKeyboard.TabIndex = 1;
            this.miniKeyboard.X = "×";
            // 
            // frmPay
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnMCardPay);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnAlter);
            this.Controls.Add(this.dgvCardPay);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.miniKeyboard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPay";
            this.Text = "结算支付界面";
            this.Load += new System.EventHandler(this.frmPay_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCardPay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MiniKeyboard miniKeyboard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbCash;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbChange;
        private System.Windows.Forms.TextBox txtPayCash;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbGoodsCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbMoneyTotal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvCardPay;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnAlter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbCardPay;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label lbYH;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.Button btnMCardPay;
    }
}