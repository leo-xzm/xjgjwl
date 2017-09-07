using POSApplication.BLL;
using POSApplication.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POSApplication.UI
{
    public partial class frmRefundPow : Form
    {

        /// <summary>
        /// 主管
        /// </summary>
        private Staff sup;


        private Staff staff;
        private frmMain main;

        //存放小键盘所按下的键值
        private string keyInput;
        //小键盘的位置
        private Point IDLocation = new Point(243, 244);
        public frmRefundPow(Staff staff, frmMain main)
        {
            InitializeComponent();
            this.staff = staff;
            this.main = main;

            //绑定小键盘事件
            miniKeyboard1.Press += MiniKeyboardHandler;
        }

        private void frmRefundPow_Load(object sender, EventArgs e)
        {

        }


        private void MiniKeyboardHandler(object sender, MiniKeyboardArgs e)
        {
            keyInput = e.KeyCode;

            // miniKeyboard1.Location = IDLocation;
            KeyboardInput(txtPow);
            txtPow.PasswordChar = '*';

        }


        private void KeyboardInput(TextBox focusing)
        {

            int startDel = 0;

            //退格
            if (keyInput == miniKeyboard1.Backspace)
            {
                if (focusing.SelectedText != "")
                    focusing.SelectedText = "";
                else if (focusing.SelectionStart > 0)
                {
                    startDel = focusing.SelectionStart;
                    focusing.Text = focusing.Text.Substring(0, focusing.SelectionStart - 1) +
                        focusing.Text.Substring(focusing.SelectionStart, focusing.Text.Length - focusing.SelectionStart);
                    focusing.SelectionStart = startDel - 1;
                }
            }
            //取消
            else if (keyInput == miniKeyboard1.Cancel)
            {
                focusing.Text = "";
            }

                //确定
            else if (keyInput == miniKeyboard1.KeyEnter)
            {

                string pwd = focusing.Text;
                //根据密码获取主管
                sup = new StaffBLL().GetStaffByPwd(pwd);
                if (sup != null)
                {
                    frmRefund frm = new frmRefund(staff, main);
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("密码不正确");
                }
            }


      //其他键直接输入
            else
            {
                if (focusing.SelectedText != "")
                    focusing.SelectedText = keyInput;
                else
                    focusing.SelectedText += keyInput;
            }

            //按键完毕，保持焦点
            focusing.Focus();
        }




    }
}
