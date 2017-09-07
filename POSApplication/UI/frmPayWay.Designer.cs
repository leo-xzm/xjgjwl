namespace POSApplication.UI
{
    partial class frmPayWay
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
            this.lbMName = new System.Windows.Forms.Label();
            this.lbBalance = new System.Windows.Forms.Label();
            this.btnCard = new System.Windows.Forms.Button();
            this.btnCash = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbMName
            // 
            this.lbMName.AutoSize = true;
            this.lbMName.Font = new System.Drawing.Font("宋体", 20F);
            this.lbMName.Location = new System.Drawing.Point(36, 55);
            this.lbMName.Name = "lbMName";
            this.lbMName.Size = new System.Drawing.Size(96, 27);
            this.lbMName.TabIndex = 0;
            this.lbMName.Text = "label1";
            // 
            // lbBalance
            // 
            this.lbBalance.AutoSize = true;
            this.lbBalance.Font = new System.Drawing.Font("宋体", 20F);
            this.lbBalance.Location = new System.Drawing.Point(36, 103);
            this.lbBalance.Name = "lbBalance";
            this.lbBalance.Size = new System.Drawing.Size(96, 27);
            this.lbBalance.TabIndex = 1;
            this.lbBalance.Text = "label2";
            // 
            // btnCard
            // 
            this.btnCard.Font = new System.Drawing.Font("宋体", 12F);
            this.btnCard.Location = new System.Drawing.Point(12, 164);
            this.btnCard.Name = "btnCard";
            this.btnCard.Size = new System.Drawing.Size(105, 65);
            this.btnCard.TabIndex = 2;
            this.btnCard.Text = "卡付";
            this.btnCard.UseVisualStyleBackColor = true;
            this.btnCard.Click += new System.EventHandler(this.btnCard_Click);
            // 
            // btnCash
            // 
            this.btnCash.Font = new System.Drawing.Font("宋体", 12F);
            this.btnCash.Location = new System.Drawing.Point(146, 164);
            this.btnCash.Name = "btnCash";
            this.btnCash.Size = new System.Drawing.Size(105, 65);
            this.btnCash.TabIndex = 3;
            this.btnCash.Text = "现金";
            this.btnCash.UseVisualStyleBackColor = true;
            this.btnCash.Click += new System.EventHandler(this.btnCash_Click);
            // 
            // frmPayWay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.ControlBox = false;
            this.Controls.Add(this.btnCash);
            this.Controls.Add(this.btnCard);
            this.Controls.Add(this.lbBalance);
            this.Controls.Add(this.lbMName);
            this.Name = "frmPayWay";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbMName;
        private System.Windows.Forms.Label lbBalance;
        private System.Windows.Forms.Button btnCard;
        private System.Windows.Forms.Button btnCash;
    }
}