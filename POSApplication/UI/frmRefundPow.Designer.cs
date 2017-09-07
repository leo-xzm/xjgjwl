namespace POSApplication.UI
{
    partial class frmRefundPow
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPow = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.miniKeyboard1 = new POSApplication.MiniKeyboard();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 16F);
            this.label1.Location = new System.Drawing.Point(16, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "密码：";
            // 
            // txtPow
            // 
            this.txtPow.Font = new System.Drawing.Font("宋体", 16F);
            this.txtPow.Location = new System.Drawing.Point(94, 36);
            this.txtPow.Name = "txtPow";
            this.txtPow.Size = new System.Drawing.Size(129, 32);
            this.txtPow.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(50, 114);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(68, 32);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(155, 114);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(68, 32);
            this.btnCancle.TabIndex = 3;
            this.btnCancle.Text = "取消 ";
            this.btnCancle.UseVisualStyleBackColor = true;
            // 
            // miniKeyboard1
            // 
            this.miniKeyboard1.BackColor = System.Drawing.SystemColors.Window;
            this.miniKeyboard1.Backspace = "退格";
            this.miniKeyboard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.miniKeyboard1.Cancel = "取消";
            this.miniKeyboard1.Dot = ".";
            this.miniKeyboard1.KeyEnter = "确定";
            this.miniKeyboard1.Location = new System.Drawing.Point(20, 79);
            this.miniKeyboard1.Name = "miniKeyboard1";
            this.miniKeyboard1.Size = new System.Drawing.Size(311, 278);
            this.miniKeyboard1.TabIndex = 4;
            this.miniKeyboard1.X = "×";
            // 
            // frmRefundPow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 369);
            this.Controls.Add(this.miniKeyboard1);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtPow);
            this.Controls.Add(this.label1);
            this.Name = "frmRefundPow";
            this.Load += new System.EventHandler(this.frmRefundPow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPow;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancle;
        private MiniKeyboard miniKeyboard1;
    }
}