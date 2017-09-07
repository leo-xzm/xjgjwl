namespace POSApplication.UI
{
    partial class frmCash
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
            this.miniKeyboard = new POSApplication.MiniKeyboard();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbZJ = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbCheck = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbCash = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.gbCash = new System.Windows.Forms.GroupBox();
            this.lb001 = new System.Windows.Forms.Label();
            this.btn001 = new System.Windows.Forms.Button();
            this.lb002 = new System.Windows.Forms.Label();
            this.btn002 = new System.Windows.Forms.Button();
            this.lb005 = new System.Windows.Forms.Label();
            this.btn005 = new System.Windows.Forms.Button();
            this.lb01 = new System.Windows.Forms.Label();
            this.btn01 = new System.Windows.Forms.Button();
            this.lb02 = new System.Windows.Forms.Label();
            this.btn02 = new System.Windows.Forms.Button();
            this.lb05 = new System.Windows.Forms.Label();
            this.btn05 = new System.Windows.Forms.Button();
            this.lb1 = new System.Windows.Forms.Label();
            this.btn1 = new System.Windows.Forms.Button();
            this.lb2 = new System.Windows.Forms.Label();
            this.btn2 = new System.Windows.Forms.Button();
            this.lb5 = new System.Windows.Forms.Label();
            this.btn5 = new System.Windows.Forms.Button();
            this.lb10 = new System.Windows.Forms.Label();
            this.btn10 = new System.Windows.Forms.Button();
            this.lb20 = new System.Windows.Forms.Label();
            this.btn20 = new System.Windows.Forms.Button();
            this.lb50 = new System.Windows.Forms.Label();
            this.btn50 = new System.Windows.Forms.Button();
            this.lb100 = new System.Windows.Forms.Label();
            this.btn100 = new System.Windows.Forms.Button();
            this.gbCheck = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCheck = new System.Windows.Forms.TextBox();
            this.btnPrintSalesDay = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.gbCash.SuspendLayout();
            this.gbCheck.SuspendLayout();
            this.SuspendLayout();
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
            this.miniKeyboard.TabIndex = 2;
            this.miniKeyboard.X = "×";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lbZJ);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.lbCheck);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.lbCash);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Location = new System.Drawing.Point(479, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(311, 172);
            this.panel2.TabIndex = 11;
            // 
            // lbZJ
            // 
            this.lbZJ.AutoSize = true;
            this.lbZJ.Font = new System.Drawing.Font("宋体", 30F);
            this.lbZJ.Location = new System.Drawing.Point(124, 119);
            this.lbZJ.Name = "lbZJ";
            this.lbZJ.Size = new System.Drawing.Size(97, 40);
            this.lbZJ.TabIndex = 15;
            this.lbZJ.Text = "0.00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 25F);
            this.label9.Location = new System.Drawing.Point(3, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(134, 34);
            this.label9.TabIndex = 14;
            this.label9.Text = "总计:￥";
            // 
            // lbCheck
            // 
            this.lbCheck.AutoSize = true;
            this.lbCheck.Font = new System.Drawing.Font("宋体", 30F);
            this.lbCheck.Location = new System.Drawing.Point(124, 70);
            this.lbCheck.Name = "lbCheck";
            this.lbCheck.Size = new System.Drawing.Size(97, 40);
            this.lbCheck.TabIndex = 13;
            this.lbCheck.Text = "0.00";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 25F);
            this.label6.Location = new System.Drawing.Point(3, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 34);
            this.label6.TabIndex = 12;
            this.label6.Text = "支票:￥";
            // 
            // lbCash
            // 
            this.lbCash.AutoSize = true;
            this.lbCash.Font = new System.Drawing.Font("宋体", 30F);
            this.lbCash.Location = new System.Drawing.Point(124, 16);
            this.lbCash.Name = "lbCash";
            this.lbCash.Size = new System.Drawing.Size(97, 40);
            this.lbCash.TabIndex = 11;
            this.lbCash.Text = "0.00";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 25F);
            this.label8.Location = new System.Drawing.Point(3, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(134, 34);
            this.label8.TabIndex = 10;
            this.label8.Text = "现金:￥";
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReturn.Font = new System.Drawing.Font("宋体", 11F);
            this.btnReturn.Location = new System.Drawing.Point(12, 12);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(48, 33);
            this.btnReturn.TabIndex = 10;
            this.btnReturn.Text = "返回";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.AliceBlue;
            this.btnPrint.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold);
            this.btnPrint.Location = new System.Drawing.Point(357, 467);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(111, 86);
            this.btnPrint.TabIndex = 18;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // gbCash
            // 
            this.gbCash.Controls.Add(this.lb001);
            this.gbCash.Controls.Add(this.btn001);
            this.gbCash.Controls.Add(this.lb002);
            this.gbCash.Controls.Add(this.btn002);
            this.gbCash.Controls.Add(this.lb005);
            this.gbCash.Controls.Add(this.btn005);
            this.gbCash.Controls.Add(this.lb01);
            this.gbCash.Controls.Add(this.btn01);
            this.gbCash.Controls.Add(this.lb02);
            this.gbCash.Controls.Add(this.btn02);
            this.gbCash.Controls.Add(this.lb05);
            this.gbCash.Controls.Add(this.btn05);
            this.gbCash.Controls.Add(this.lb1);
            this.gbCash.Controls.Add(this.btn1);
            this.gbCash.Controls.Add(this.lb2);
            this.gbCash.Controls.Add(this.btn2);
            this.gbCash.Controls.Add(this.lb5);
            this.gbCash.Controls.Add(this.btn5);
            this.gbCash.Controls.Add(this.lb10);
            this.gbCash.Controls.Add(this.btn10);
            this.gbCash.Controls.Add(this.lb20);
            this.gbCash.Controls.Add(this.btn20);
            this.gbCash.Controls.Add(this.lb50);
            this.gbCash.Controls.Add(this.btn50);
            this.gbCash.Controls.Add(this.lb100);
            this.gbCash.Controls.Add(this.btn100);
            this.gbCash.Font = new System.Drawing.Font("宋体", 25F);
            this.gbCash.Location = new System.Drawing.Point(12, 59);
            this.gbCash.Name = "gbCash";
            this.gbCash.Size = new System.Drawing.Size(456, 374);
            this.gbCash.TabIndex = 19;
            this.gbCash.TabStop = false;
            this.gbCash.Text = "现金";
            // 
            // lb001
            // 
            this.lb001.AutoSize = true;
            this.lb001.Font = new System.Drawing.Font("宋体", 12F);
            this.lb001.Location = new System.Drawing.Point(96, 319);
            this.lb001.Name = "lb001";
            this.lb001.Size = new System.Drawing.Size(40, 16);
            this.lb001.TabIndex = 67;
            this.lb001.Text = "× 0";
            // 
            // btn001
            // 
            this.btn001.BackColor = System.Drawing.Color.AliceBlue;
            this.btn001.Font = new System.Drawing.Font("宋体", 12F);
            this.btn001.Location = new System.Drawing.Point(23, 309);
            this.btn001.Name = "btn001";
            this.btn001.Size = new System.Drawing.Size(67, 37);
            this.btn001.TabIndex = 66;
            this.btn001.Text = "0.01元";
            this.btn001.UseVisualStyleBackColor = false;
            // 
            // lb002
            // 
            this.lb002.AutoSize = true;
            this.lb002.Font = new System.Drawing.Font("宋体", 12F);
            this.lb002.Location = new System.Drawing.Point(388, 256);
            this.lb002.Name = "lb002";
            this.lb002.Size = new System.Drawing.Size(40, 16);
            this.lb002.TabIndex = 65;
            this.lb002.Text = "× 0";
            // 
            // btn002
            // 
            this.btn002.BackColor = System.Drawing.Color.AliceBlue;
            this.btn002.Font = new System.Drawing.Font("宋体", 12F);
            this.btn002.Location = new System.Drawing.Point(315, 246);
            this.btn002.Name = "btn002";
            this.btn002.Size = new System.Drawing.Size(67, 37);
            this.btn002.TabIndex = 64;
            this.btn002.Text = "0.02元";
            this.btn002.UseVisualStyleBackColor = false;
            // 
            // lb005
            // 
            this.lb005.AutoSize = true;
            this.lb005.Font = new System.Drawing.Font("宋体", 12F);
            this.lb005.Location = new System.Drawing.Point(242, 256);
            this.lb005.Name = "lb005";
            this.lb005.Size = new System.Drawing.Size(40, 16);
            this.lb005.TabIndex = 63;
            this.lb005.Text = "× 0";
            // 
            // btn005
            // 
            this.btn005.BackColor = System.Drawing.Color.AliceBlue;
            this.btn005.Font = new System.Drawing.Font("宋体", 12F);
            this.btn005.Location = new System.Drawing.Point(169, 246);
            this.btn005.Name = "btn005";
            this.btn005.Size = new System.Drawing.Size(67, 37);
            this.btn005.TabIndex = 62;
            this.btn005.Text = "0.05元";
            this.btn005.UseVisualStyleBackColor = false;
            // 
            // lb01
            // 
            this.lb01.AutoSize = true;
            this.lb01.Font = new System.Drawing.Font("宋体", 12F);
            this.lb01.Location = new System.Drawing.Point(96, 256);
            this.lb01.Name = "lb01";
            this.lb01.Size = new System.Drawing.Size(40, 16);
            this.lb01.TabIndex = 61;
            this.lb01.Text = "× 0";
            // 
            // btn01
            // 
            this.btn01.BackColor = System.Drawing.Color.AliceBlue;
            this.btn01.Font = new System.Drawing.Font("宋体", 12F);
            this.btn01.Location = new System.Drawing.Point(23, 246);
            this.btn01.Name = "btn01";
            this.btn01.Size = new System.Drawing.Size(67, 37);
            this.btn01.TabIndex = 60;
            this.btn01.Text = "0.1元";
            this.btn01.UseVisualStyleBackColor = false;
            // 
            // lb02
            // 
            this.lb02.AutoSize = true;
            this.lb02.Font = new System.Drawing.Font("宋体", 12F);
            this.lb02.Location = new System.Drawing.Point(388, 193);
            this.lb02.Name = "lb02";
            this.lb02.Size = new System.Drawing.Size(40, 16);
            this.lb02.TabIndex = 59;
            this.lb02.Text = "× 0";
            // 
            // btn02
            // 
            this.btn02.BackColor = System.Drawing.Color.AliceBlue;
            this.btn02.Font = new System.Drawing.Font("宋体", 12F);
            this.btn02.Location = new System.Drawing.Point(315, 183);
            this.btn02.Name = "btn02";
            this.btn02.Size = new System.Drawing.Size(67, 37);
            this.btn02.TabIndex = 58;
            this.btn02.Text = "0.2元";
            this.btn02.UseVisualStyleBackColor = false;
            // 
            // lb05
            // 
            this.lb05.AutoSize = true;
            this.lb05.Font = new System.Drawing.Font("宋体", 12F);
            this.lb05.Location = new System.Drawing.Point(242, 193);
            this.lb05.Name = "lb05";
            this.lb05.Size = new System.Drawing.Size(40, 16);
            this.lb05.TabIndex = 57;
            this.lb05.Text = "× 0";
            // 
            // btn05
            // 
            this.btn05.BackColor = System.Drawing.Color.AliceBlue;
            this.btn05.Font = new System.Drawing.Font("宋体", 12F);
            this.btn05.Location = new System.Drawing.Point(169, 183);
            this.btn05.Name = "btn05";
            this.btn05.Size = new System.Drawing.Size(67, 37);
            this.btn05.TabIndex = 56;
            this.btn05.Text = "0.5元";
            this.btn05.UseVisualStyleBackColor = false;
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Font = new System.Drawing.Font("宋体", 12F);
            this.lb1.Location = new System.Drawing.Point(96, 193);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(40, 16);
            this.lb1.TabIndex = 55;
            this.lb1.Text = "× 0";
            // 
            // btn1
            // 
            this.btn1.BackColor = System.Drawing.Color.AliceBlue;
            this.btn1.Font = new System.Drawing.Font("宋体", 12F);
            this.btn1.Location = new System.Drawing.Point(23, 183);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(67, 37);
            this.btn1.TabIndex = 54;
            this.btn1.Text = "1元";
            this.btn1.UseVisualStyleBackColor = false;
            // 
            // lb2
            // 
            this.lb2.AutoSize = true;
            this.lb2.Font = new System.Drawing.Font("宋体", 12F);
            this.lb2.Location = new System.Drawing.Point(388, 130);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(40, 16);
            this.lb2.TabIndex = 53;
            this.lb2.Text = "× 0";
            // 
            // btn2
            // 
            this.btn2.BackColor = System.Drawing.Color.AliceBlue;
            this.btn2.Font = new System.Drawing.Font("宋体", 12F);
            this.btn2.Location = new System.Drawing.Point(315, 120);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(67, 37);
            this.btn2.TabIndex = 52;
            this.btn2.Text = "2元";
            this.btn2.UseVisualStyleBackColor = false;
            // 
            // lb5
            // 
            this.lb5.AutoSize = true;
            this.lb5.Font = new System.Drawing.Font("宋体", 12F);
            this.lb5.Location = new System.Drawing.Point(242, 130);
            this.lb5.Name = "lb5";
            this.lb5.Size = new System.Drawing.Size(40, 16);
            this.lb5.TabIndex = 51;
            this.lb5.Text = "× 0";
            // 
            // btn5
            // 
            this.btn5.BackColor = System.Drawing.Color.AliceBlue;
            this.btn5.Font = new System.Drawing.Font("宋体", 12F);
            this.btn5.Location = new System.Drawing.Point(169, 120);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(67, 37);
            this.btn5.TabIndex = 50;
            this.btn5.Text = "5元";
            this.btn5.UseVisualStyleBackColor = false;
            // 
            // lb10
            // 
            this.lb10.AutoSize = true;
            this.lb10.Font = new System.Drawing.Font("宋体", 12F);
            this.lb10.Location = new System.Drawing.Point(96, 130);
            this.lb10.Name = "lb10";
            this.lb10.Size = new System.Drawing.Size(40, 16);
            this.lb10.TabIndex = 49;
            this.lb10.Text = "× 0";
            // 
            // btn10
            // 
            this.btn10.BackColor = System.Drawing.Color.AliceBlue;
            this.btn10.Font = new System.Drawing.Font("宋体", 12F);
            this.btn10.Location = new System.Drawing.Point(23, 120);
            this.btn10.Name = "btn10";
            this.btn10.Size = new System.Drawing.Size(67, 37);
            this.btn10.TabIndex = 48;
            this.btn10.Text = "10元";
            this.btn10.UseVisualStyleBackColor = false;
            // 
            // lb20
            // 
            this.lb20.AutoSize = true;
            this.lb20.Font = new System.Drawing.Font("宋体", 12F);
            this.lb20.Location = new System.Drawing.Point(388, 67);
            this.lb20.Name = "lb20";
            this.lb20.Size = new System.Drawing.Size(40, 16);
            this.lb20.TabIndex = 47;
            this.lb20.Text = "× 0";
            // 
            // btn20
            // 
            this.btn20.BackColor = System.Drawing.Color.AliceBlue;
            this.btn20.Font = new System.Drawing.Font("宋体", 12F);
            this.btn20.Location = new System.Drawing.Point(315, 57);
            this.btn20.Name = "btn20";
            this.btn20.Size = new System.Drawing.Size(67, 37);
            this.btn20.TabIndex = 46;
            this.btn20.Text = "20元";
            this.btn20.UseVisualStyleBackColor = false;
            // 
            // lb50
            // 
            this.lb50.AutoSize = true;
            this.lb50.Font = new System.Drawing.Font("宋体", 12F);
            this.lb50.Location = new System.Drawing.Point(242, 67);
            this.lb50.Name = "lb50";
            this.lb50.Size = new System.Drawing.Size(40, 16);
            this.lb50.TabIndex = 45;
            this.lb50.Text = "× 0";
            // 
            // btn50
            // 
            this.btn50.BackColor = System.Drawing.Color.AliceBlue;
            this.btn50.Font = new System.Drawing.Font("宋体", 12F);
            this.btn50.Location = new System.Drawing.Point(169, 57);
            this.btn50.Name = "btn50";
            this.btn50.Size = new System.Drawing.Size(67, 37);
            this.btn50.TabIndex = 44;
            this.btn50.Text = "50元";
            this.btn50.UseVisualStyleBackColor = false;
            // 
            // lb100
            // 
            this.lb100.AutoSize = true;
            this.lb100.Font = new System.Drawing.Font("宋体", 12F);
            this.lb100.Location = new System.Drawing.Point(96, 67);
            this.lb100.Name = "lb100";
            this.lb100.Size = new System.Drawing.Size(40, 16);
            this.lb100.TabIndex = 43;
            this.lb100.Text = "× 0";
            // 
            // btn100
            // 
            this.btn100.BackColor = System.Drawing.Color.AliceBlue;
            this.btn100.Font = new System.Drawing.Font("宋体", 12F);
            this.btn100.Location = new System.Drawing.Point(23, 57);
            this.btn100.Name = "btn100";
            this.btn100.Size = new System.Drawing.Size(67, 37);
            this.btn100.TabIndex = 42;
            this.btn100.Text = "100元";
            this.btn100.UseVisualStyleBackColor = false;
            // 
            // gbCheck
            // 
            this.gbCheck.Controls.Add(this.label1);
            this.gbCheck.Controls.Add(this.txtCheck);
            this.gbCheck.Font = new System.Drawing.Font("宋体", 25F);
            this.gbCheck.Location = new System.Drawing.Point(12, 453);
            this.gbCheck.Name = "gbCheck";
            this.gbCheck.Size = new System.Drawing.Size(327, 100);
            this.gbCheck.TabIndex = 68;
            this.gbCheck.TabStop = false;
            this.gbCheck.Text = "支票";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 25F);
            this.label1.Location = new System.Drawing.Point(53, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 34);
            this.label1.TabIndex = 16;
            this.label1.Text = "￥";
            // 
            // txtCheck
            // 
            this.txtCheck.Location = new System.Drawing.Point(108, 40);
            this.txtCheck.Name = "txtCheck";
            this.txtCheck.Size = new System.Drawing.Size(174, 46);
            this.txtCheck.TabIndex = 0;
            this.txtCheck.Text = "0.00";
            this.txtCheck.TextChanged += new System.EventHandler(this.txtCheck_TextChanged);
            // 
            // btnPrintSalesDay
            // 
            this.btnPrintSalesDay.BackColor = System.Drawing.Color.AliceBlue;
            this.btnPrintSalesDay.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
            this.btnPrintSalesDay.Location = new System.Drawing.Point(489, 467);
            this.btnPrintSalesDay.Name = "btnPrintSalesDay";
            this.btnPrintSalesDay.Size = new System.Drawing.Size(136, 86);
            this.btnPrintSalesDay.TabIndex = 69;
            this.btnPrintSalesDay.Text = "本机当日销售汇总";
            this.btnPrintSalesDay.UseVisualStyleBackColor = false;
            this.btnPrintSalesDay.Click += new System.EventHandler(this.btnPrintSalesDay_Click);
            // 
            // frmCash
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnPrintSalesDay);
            this.Controls.Add(this.gbCheck);
            this.Controls.Add(this.gbCash);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.miniKeyboard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCash";
            this.Text = "解款打印界面";
            this.Load += new System.EventHandler(this.frmCash_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.gbCash.ResumeLayout(false);
            this.gbCash.PerformLayout();
            this.gbCheck.ResumeLayout(false);
            this.gbCheck.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MiniKeyboard miniKeyboard;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbCash;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbZJ;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbCheck;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.GroupBox gbCash;
        private System.Windows.Forms.Label lb001;
        private System.Windows.Forms.Button btn001;
        private System.Windows.Forms.Label lb002;
        private System.Windows.Forms.Button btn002;
        private System.Windows.Forms.Label lb005;
        private System.Windows.Forms.Button btn005;
        private System.Windows.Forms.Label lb01;
        private System.Windows.Forms.Button btn01;
        private System.Windows.Forms.Label lb02;
        private System.Windows.Forms.Button btn02;
        private System.Windows.Forms.Label lb05;
        private System.Windows.Forms.Button btn05;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Label lb2;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Label lb5;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Label lb10;
        private System.Windows.Forms.Button btn10;
        private System.Windows.Forms.Label lb20;
        private System.Windows.Forms.Button btn20;
        private System.Windows.Forms.Label lb50;
        private System.Windows.Forms.Button btn50;
        private System.Windows.Forms.Label lb100;
        private System.Windows.Forms.Button btn100;
        private System.Windows.Forms.GroupBox gbCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCheck;
        private System.Windows.Forms.Button btnPrintSalesDay;
    }
}