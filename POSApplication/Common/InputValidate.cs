using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POSApplication.Common
{
    public static class InputValidate
    {
        /// <summary>
        /// 退格键的逻辑
        /// </summary>
        /// <param name="txtPayCash"></param>
        public static void InputBackspace(TextBox focusing)
        {
            int startDel = 0;

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

        /// <summary>
        /// 打折输入框，输入时进行格式规范
        /// </summary>
        /// <param name="txtInput"></param>
        public static void DiscountInputValidate(TextBox txtInput)
        {
            decimal discount = 0;
            txtInput.Text = txtInput.Text.TrimStart('.').Replace("..", ".").Replace("00", "0");

            if (txtInput.TextLength > 0)
            {
                //小数点后只保留两位
                if (txtInput.Text.Contains('.') && (txtInput.TextLength - txtInput.Text.IndexOf('.') > 3))
                    txtInput.Text = txtInput.Text.Substring(0, txtInput.Text.IndexOf('.') + 3);

                //已输入过小数点后，不能再输入小数点
                if (txtInput.Text.Substring(0, txtInput.TextLength - 1).Contains('.'))
                    txtInput.Text = txtInput.Text.TrimEnd('.');

                //导致最后结果大于等于1的输入，都改为小数
                discount = Convert.ToDecimal(txtInput.Text.TrimEnd('.'));

                if (discount >= 1)
                   txtInput.Text = "0."+discount.ToString();
            }

            //小数点后只保留两位(0.xx)
            if (txtInput.TextLength > 4)
                txtInput.Text = txtInput.Text.Substring(0, 4);

            txtInput.SelectionStart = txtInput.TextLength;
        }

        /// <summary>
        /// 现金格式输入规范，小数点后只能输入两位
        /// </summary>
        /// <param name="txtPayCash"></param>
        public static void CashInputValidate(TextBox txtCash)
        {
            //过滤两个小数点
            if (txtCash.Text.Contains(".."))
                txtCash.Text = txtCash.Text.Replace("..", ".");

            if (txtCash.TextLength > 0)
            {
                //第一位为"."时，自动在前面添加 0
                if (txtCash.Text.Substring(0, 1) == ".")
                    txtCash.Text = "0." + txtCash.Text.Substring(1, txtCash.TextLength - 1);

                //已输入过小数点后，不能再输入小数点
                if (txtCash.Text.Substring(0, txtCash.TextLength - 1).Contains('.'))
                    txtCash.Text = txtCash.Text.TrimEnd('.');

                //小数点后只保留两位
                if (txtCash.Text.Contains('.') && (txtCash.TextLength - txtCash.Text.IndexOf('.') > 3))
                    txtCash.Text = txtCash.Text.Substring(0, txtCash.Text.IndexOf('.') + 3);
            }
            if (txtCash.TextLength > 1)
            {
                //第一位输入0后，第二位自动加小数点
                if (txtCash.Text.Substring(0, 1) == "0" && txtCash.Text.Substring(0, 2) != "0." )
                {
                    if (txtCash.Text.Substring(1, 1) != ".")
                        txtCash.Text = "0." + txtCash.Text.Substring(1,txtCash.TextLength-1);
                }
            }
        }

        /// <summary>
        /// 支付输入规范，小数点后只能输入一位
        /// </summary>
        /// <param name="txtPayCash"></param>
        public static void PayInputValidate(TextBox txtCash)
        {
            //过滤两个小数点
            if (txtCash.Text.Contains(".."))
                txtCash.Text = txtCash.Text.Replace("..", ".");

            if (txtCash.TextLength > 0)
            {
                //第一位为"."时，自动在前面添加 0
                if (txtCash.Text.Substring(0, 1) == ".")
                    txtCash.Text = "0." + txtCash.Text.Substring(1, txtCash.TextLength - 1);

                //已输入过小数点后，不能再输入小数点
                if (txtCash.Text.Substring(0, txtCash.TextLength - 1).Contains('.'))
                    txtCash.Text = txtCash.Text.TrimEnd('.');

                //小数点后只保留一位
                if (txtCash.Text.Contains('.') && (txtCash.TextLength - txtCash.Text.IndexOf('.') > 2))
                    txtCash.Text = txtCash.Text.Substring(0, txtCash.Text.IndexOf('.') + 2);
            }
            if (txtCash.TextLength > 1)
            {
                //第一位输入0后，第二位自动加小数点
                if (txtCash.Text.Substring(0, 1) == "0" && txtCash.Text.Substring(0, 2) != "0.")
                {
                    if (txtCash.Text.Substring(1, 1) != ".")
                        txtCash.Text = "0." + txtCash.Text.Substring(1, txtCash.TextLength - 1);
                }
            }
        }
    }
}
