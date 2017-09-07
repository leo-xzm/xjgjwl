using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POSApplication.Model;
using POSApplication.Common;

namespace POSApplication.UserController
{
    public partial class InputBox : UserControl
    {
        public OrderHandleType type;

        /// <summary>
        /// 输入密码，type 为 password；整单打折，type 为 discountOverall；解款打印，type 为 cash
        /// </summary>
        /// <param name="type"></param>
        public InputBox(OrderHandleType type)
        {
            InitializeComponent();
            this.type = type;
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            switch(type)
            {
                case OrderHandleType.password:
                    lbTitle.Text = "密码：";
                    txtInput.PasswordChar = '*';
                    break;
                case OrderHandleType.discountOverall:
                    lbTitle.Text = "整单打折：";
                    break;
                case OrderHandleType.cash:
                    lbTitle.Text = "数量：";
                    break;
            }
        }

        //整单打折时，规范输入格式
        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            if (type == OrderHandleType.discountOverall)
                InputValidate.DiscountInputValidate(txtInput);
        }
    }
}
