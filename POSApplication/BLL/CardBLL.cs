using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POSApplication.Model;
using POSApplication.DAL;

namespace POSApplication.BLL
{
    public class CardBLL
    {
        /// <summary>
        /// 获取支付卡种类
        /// </summary>
        /// <returns></returns>
        public List<Card> GetCards()
        {
            return new CardDAL().GetCards();
        }

        /// <summary>
        /// 清空结算方式表
        /// </summary>
        /// <returns></returns>
        public int DeleteCard()
        {
            return new CardDAL().DeleteCard();
        }
    }
}
