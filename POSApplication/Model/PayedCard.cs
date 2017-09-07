using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POSApplication.BLL;
using System.Windows.Forms;

namespace POSApplication.Model
{
    public class PayedCard: Card
    {
        /// <summary>
        /// 刷卡金额
        /// </summary>
        public decimal je { get; set; }

        public PayedCard(Card card)
        {
            this.id = card.id;
            this.card = card.card;
            this.jsfsbh = card.jsfsbh;
            this.State = card.State;
            this.ksrq = card.ksrq;
        }

        //根据结算方式编号获取卡实体
        public PayedCard(string jsfsbh)
        {
            Card mCard = new Card();
            List<Card> list = new CardBLL().GetCards();

            mCard = list.Where(o => o.jsfsbh == jsfsbh).FirstOrDefault();

            if (mCard != null)
            {
                this.id = mCard.id;
                this.card = mCard.card;
                this.jsfsbh = mCard.jsfsbh;
                this.State = mCard.State;
                this.ksrq = mCard.ksrq;
            }
            else
            {
                MessageBox.Show("结算方式编号不正确，没有查到实体卡");
            }
        }
    }
}
