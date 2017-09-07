namespace POSApplication.UserController
{
    partial class GoodsFrame
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
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbTxm = new System.Windows.Forms.Label();
            this.lbDiscount = new System.Windows.Forms.Label();
            this.lbSubtotal = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbCode = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.lbCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbSellPrice = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEnter
            // 
            this.btnEnter.BackColor = System.Drawing.Color.AliceBlue;
            this.btnEnter.Font = new System.Drawing.Font("宋体", 15F);
            this.btnEnter.Location = new System.Drawing.Point(102, 269);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(111, 46);
            this.btnEnter.TabIndex = 14;
            this.btnEnter.Text = "确定";
            this.btnEnter.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 15F);
            this.btnCancel.Location = new System.Drawing.Point(242, 269);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(111, 46);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtDiscount
            // 
            this.txtDiscount.Font = new System.Drawing.Font("宋体", 25F);
            this.txtDiscount.Location = new System.Drawing.Point(102, 206);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(81, 46);
            this.txtDiscount.TabIndex = 43;
            this.txtDiscount.TextChanged += new System.EventHandler(this.txtDiscount_TextChanged);
            // 
            // txtCount
            // 
            this.txtCount.Font = new System.Drawing.Font("宋体", 25F);
            this.txtCount.Location = new System.Drawing.Point(102, 152);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(81, 46);
            this.txtCount.TabIndex = 42;
            this.txtCount.TextChanged += new System.EventHandler(this.txtCount_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 20F);
            this.label6.Location = new System.Drawing.Point(21, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 27);
            this.label6.TabIndex = 40;
            this.label6.Text = "条码:";
            // 
            // lbTxm
            // 
            this.lbTxm.AutoSize = true;
            this.lbTxm.Font = new System.Drawing.Font("宋体", 20F);
            this.lbTxm.Location = new System.Drawing.Point(97, 62);
            this.lbTxm.Name = "lbTxm";
            this.lbTxm.Size = new System.Drawing.Size(194, 27);
            this.lbTxm.TabIndex = 41;
            this.lbTxm.Text = "1234567891234";
            // 
            // lbDiscount
            // 
            this.lbDiscount.AutoSize = true;
            this.lbDiscount.Font = new System.Drawing.Font("宋体", 20F);
            this.lbDiscount.Location = new System.Drawing.Point(97, 214);
            this.lbDiscount.Name = "lbDiscount";
            this.lbDiscount.Size = new System.Drawing.Size(54, 27);
            this.lbDiscount.TabIndex = 39;
            this.lbDiscount.Text = "1.0";
            // 
            // lbSubtotal
            // 
            this.lbSubtotal.AutoSize = true;
            this.lbSubtotal.Font = new System.Drawing.Font("宋体", 20F);
            this.lbSubtotal.Location = new System.Drawing.Point(276, 214);
            this.lbSubtotal.Name = "lbSubtotal";
            this.lbSubtotal.Size = new System.Drawing.Size(54, 27);
            this.lbSubtotal.TabIndex = 37;
            this.lbSubtotal.Text = "333";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 20F);
            this.label5.Location = new System.Drawing.Point(21, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 27);
            this.label5.TabIndex = 38;
            this.label5.Text = "打折:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 20F);
            this.label1.Location = new System.Drawing.Point(21, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 27);
            this.label1.TabIndex = 30;
            this.label1.Text = "货号:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 20F);
            this.label4.Location = new System.Drawing.Point(195, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 27);
            this.label4.TabIndex = 36;
            this.label4.Text = "小计:";
            // 
            // lbCode
            // 
            this.lbCode.AutoSize = true;
            this.lbCode.Font = new System.Drawing.Font("宋体", 20F);
            this.lbCode.Location = new System.Drawing.Point(97, 107);
            this.lbCode.Name = "lbCode";
            this.lbCode.Size = new System.Drawing.Size(82, 27);
            this.lbCode.TabIndex = 31;
            this.lbCode.Text = "11111";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 20F);
            this.label2.Location = new System.Drawing.Point(195, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 27);
            this.label2.TabIndex = 32;
            this.label2.Text = "商品:";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 20F);
            this.lbName.Location = new System.Drawing.Point(276, 107);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(120, 27);
            this.lbName.TabIndex = 33;
            this.lbName.Text = "哈根达斯";
            // 
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Font = new System.Drawing.Font("宋体", 20F);
            this.lbCount.Location = new System.Drawing.Point(102, 160);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(40, 27);
            this.lbCount.TabIndex = 35;
            this.lbCount.Text = "10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 20F);
            this.label3.Location = new System.Drawing.Point(21, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 27);
            this.label3.TabIndex = 34;
            this.label3.Text = "数量:";
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("宋体", 25F);
            this.lbTitle.Location = new System.Drawing.Point(3, 12);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(100, 34);
            this.lbTitle.TabIndex = 44;
            this.lbTitle.Text = "Title";
            // 
            // lbSellPrice
            // 
            this.lbSellPrice.AutoSize = true;
            this.lbSellPrice.Font = new System.Drawing.Font("宋体", 20F);
            this.lbSellPrice.Location = new System.Drawing.Point(276, 160);
            this.lbSellPrice.Name = "lbSellPrice";
            this.lbSellPrice.Size = new System.Drawing.Size(54, 27);
            this.lbSellPrice.TabIndex = 46;
            this.lbSellPrice.Text = "333";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 20F);
            this.label8.Location = new System.Drawing.Point(195, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 27);
            this.label8.TabIndex = 45;
            this.label8.Text = "单价:";
            // 
            // GoodsFrame
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lbSellPrice);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbTxm);
            this.Controls.Add(this.lbDiscount);
            this.Controls.Add(this.lbSubtotal);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.lbCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.btnCancel);
            this.Name = "GoodsFrame";
            this.Size = new System.Drawing.Size(472, 260);
            this.Load += new System.EventHandler(this.GoodsFrame_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbTxm;
        private System.Windows.Forms.Label lbDiscount;
        private System.Windows.Forms.Label lbSubtotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbSellPrice;
        private System.Windows.Forms.Label label8;
    }
}
