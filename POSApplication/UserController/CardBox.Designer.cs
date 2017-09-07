namespace POSApplication.UserController
{
    partial class CardBox
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
            this.txtCardCash = new System.Windows.Forms.TextBox();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbCard = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelCardCash.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCardCash
            // 
            this.panelCardCash.BackColor = System.Drawing.SystemColors.Info;
            this.panelCardCash.Controls.Add(this.txtCardCash);
            this.panelCardCash.Controls.Add(this.btnEnter);
            this.panelCardCash.Controls.Add(this.btnCancel);
            this.panelCardCash.Controls.Add(this.label3);
            this.panelCardCash.Controls.Add(this.lbCard);
            this.panelCardCash.Controls.Add(this.label1);
            this.panelCardCash.Location = new System.Drawing.Point(0, 0);
            this.panelCardCash.Name = "panelCardCash";
            this.panelCardCash.Size = new System.Drawing.Size(453, 280);
            this.panelCardCash.TabIndex = 12;
            // 
            // txtCardCash
            // 
            this.txtCardCash.Font = new System.Drawing.Font("宋体", 35F);
            this.txtCardCash.Location = new System.Drawing.Point(221, 121);
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
            this.label3.Location = new System.Drawing.Point(18, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 40);
            this.label3.TabIndex = 14;
            this.label3.Text = "支付金额:";
            // 
            // lbCard
            // 
            this.lbCard.AutoSize = true;
            this.lbCard.Font = new System.Drawing.Font("宋体", 30F);
            this.lbCard.Location = new System.Drawing.Point(207, 63);
            this.lbCard.Name = "lbCard";
            this.lbCard.Size = new System.Drawing.Size(97, 40);
            this.lbCard.TabIndex = 13;
            this.lbCard.Text = "卡名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 30F);
            this.label1.Location = new System.Drawing.Point(18, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 40);
            this.label1.TabIndex = 12;
            this.label1.Text = "支付方式:";
            // 
            // CardBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panelCardCash);
            this.Name = "CardBox";
            this.Size = new System.Drawing.Size(453, 280);
            this.panelCardCash.ResumeLayout(false);
            this.panelCardCash.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCardCash;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbCard;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtCardCash;




    }
}
