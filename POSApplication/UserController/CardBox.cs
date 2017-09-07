using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POSApplication.Model;
using POSApplication.BLL;
using POSApplication.Common;

namespace POSApplication.UserController
{
    public partial class CardBox : UserControl
    {
        private Panel panelCard;
        private int iniX = 6;
        private int iniY = 12;
        public int index = -1;

        /// <summary>
        /// 卡列表
        /// </summary>
        public List<Card> cardList;

        /// <summary>
        /// 待付金额
        /// </summary>
        public decimal MoneyTotal;

        public CardBox()
        {
            InitializeComponent();
            panelCardShow();
            panelCardCash.Hide();
        }

        //修改支付金额，直接传入 payedCard 实体
        public CardBox(PayedCard payedCard, decimal moneyTotal)
        {
            InitializeComponent();
            panelCardShow();
            panelCard.Hide();

            Card alterCard = cardList.Where(c => c.id == payedCard.id).FirstOrDefault();

            index = cardList.IndexOf(alterCard);

            lbCard.Text = payedCard.card;

            MoneyTotal = moneyTotal;

            txtCardCash.Text = MoneyTotal.ToString();

            txtCardCash.SelectionStart = txtCardCash.TextLength;
            txtCardCash.Focus();
        }

        /// <summary>
        /// 显示卡片按钮列表
        /// </summary>
        private void panelCardShow()
        {
            try
            {
                int x = iniX, y = iniY;
                cardList = new CardBLL().GetCards();

                panelCard = new Panel();
                panelCard.Name = "panelCard";
                panelCard.Size = new System.Drawing.Size(447, 265);
                panelCard.Location = new System.Drawing.Point(3, 3);
                this.Controls.Add(panelCard);

                for (int i = 0; i < cardList.Count; i++)
                {
                    //按钮
                    Button btn = new Button();
                    btn.Size = new Size(81, 53);
                    btn.Location = new Point(x, y);
                    btn.Font = new Font("宋体", 12);
                    btn.BackColor = Color.AliceBlue;
                    btn.Text = cardList[i].card;
                    btn.Click += new System.EventHandler(this.btn_Click);
                    panelCard.Controls.Add(btn);

                    //定位下一个按钮，每五个换行
                    x += 88;
                    if (i % 5 == 4)
                    {
                        x = iniX;
                        y += 65;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog(ex.Message, "panelCardShow()", "CardBox.cs");
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            txtCardCash.Text = this.MoneyTotal.ToString();
            panelCardCash.Show();
            txtCardCash.Focus();
            panelCard.Hide();

            //根据点击按钮的Text查找对应的Card
            Card selectCard = cardList.Where(u => u.card == btn.Text).FirstOrDefault();
            lbCard.Text = selectCard.card;
            //获取Card在列表中的index，方便调用
            index = cardList.IndexOf(selectCard);
        }

        private void txtCardCash_TextChanged(object sender, EventArgs e)
        {
            InputValidate.PayInputValidate(txtCardCash);

            //如果输入的值大于待付金额，就改成待付金额
            if (Convert.ToDecimal(txtCardCash.Text == "" ? "0" : txtCardCash.Text) > MoneyTotal)
                txtCardCash.Text = MoneyTotal.ToString();

            txtCardCash.SelectionStart = txtCardCash.TextLength;
        }
    }
}
