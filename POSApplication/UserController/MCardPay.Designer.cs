namespace POSApplication.UserController
{
    partial class MCardPay
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
            this.panelCardCash = new System.Windows.Forms.Panel();
            this.lbBalance = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCardCash = new System.Windows.Forms.TextBox();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelCardCash.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCardCash
            // 
            this.panelCardCash.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panelCardCash.Controls.Add(this.lbBalance);
            this.panelCardCash.Controls.Add(this.label4);
            this.panelCardCash.Controls.Add(this.txtCardCash);
            this.panelCardCash.Controls.Add(this.btnEnter);
            this.panelCardCash.Controls.Add(this.btnCancel);
            this.panelCardCash.Controls.Add(this.label3);
            this.panelCardCash.Controls.Add(this.lbName);
            this.panelCardCash.Controls.Add(this.label1);
            this.panelCardCash.Location = new System.Drawing.Point(0, 0);
            this.panelCardCash.Name = "panelCardCash";
            this.panelCardCash.Size = new System.Drawing.Size(453, 280);
            this.panelCardCash.TabIndex = 13;
            // 
            // lbBalance
            // 
            this.lbBalance.AutoSize = true;
            this.lbBalance.Font = new System.Drawing.Font("宋体", 30F);
            this.lbBalance.Location = new System.Drawing.Point(214, 100);
            this.lbBalance.Name = "lbBalance";
            this.lbBalance.Size = new System.Drawing.Size(77, 40);
            this.lbBalance.TabIndex = 19;
            this.lbBalance.Text = "0.0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 30F);
            this.label4.Location = new System.Drawing.Point(58, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 40);
            this.label4.TabIndex = 18;
            this.label4.Text = "卡余额:";
            // 
            // txtCardCash
            // 
            this.txtCardCash.Font = new System.Drawing.Font("宋体", 35F);
            this.txtCardCash.Location = new System.Drawing.Point(221, 166);
            this.txtCardCash.Name = "txtCardCash";
            this.txtCardCash.Size = new System.Drawing.Size(208, 61);
            this.txtCardCash.TabIndex = 15;
            this.txtCardCash.Text = "0.0";
            this.txtCardCash.TextChanged += new System.EventHandler(this.txtCardCash_TextChanged);
            // 
            // btnEnter
            // 
            this.btnEnter.BackColor = System.Drawing.Color.AliceBlue;
            this.btnEnter.Font = new System.Drawing.Font("宋体", 15F);
            this.btnEnter.Location = new System.Drawing.Point(115, 287);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(85, 40);
            this.btnEnter.TabIndex = 16;
            this.btnEnter.Text = "确定";
            this.btnEnter.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 15F);
            this.btnCancel.Location = new System.Drawing.Point(221, 287);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 40);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 30F);
            this.label3.Location = new System.Drawing.Point(18, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 40);
            this.label3.TabIndex = 14;
            this.label3.Text = "支付金额:";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 30F);
            this.lbName.Location = new System.Drawing.Point(214, 35);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(0, 40);
            this.lbName.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 30F);
            this.label1.Location = new System.Drawing.Point(58, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 40);
            this.label1.TabIndex = 12;
            this.label1.Text = "持卡人:";
            // 
            // MCardPay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelCardCash);
            this.Name = "MCardPay";
            this.Size = new System.Drawing.Size(453, 280);
            this.Load += new System.EventHandler(this.MCardPay_Load);
            this.panelCardCash.ResumeLayout(false);
            this.panelCardCash.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCardCash;
        public System.Windows.Forms.TextBox txtCardCash;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbBalance;
        private System.Windows.Forms.Label label4;
    }
}
