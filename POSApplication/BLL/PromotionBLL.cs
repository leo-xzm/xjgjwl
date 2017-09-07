﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POSApplication.DAL;
using POSApplication.Model;
using System.Windows.Forms;

namespace POSApplication.BLL
{
    public class PromotionBLL
    {
        //处理促销
        public decimal PromotionProcessor(ref List<OrderItem> order)
        {
            decimal benefit = 0;

            string codes = string.Empty;

            if (order.Count > 0)
            {
                foreach (OrderItem item in order)
                {
                    //先都刷成非促销，再重新计算，以防止先参加促销又删除商品
                    item.cxbh = "00";
                    item.cxid = "0";
                    item.yhmx = 0;

                    //只查询有效的商品
                    if (item.isValid)
                        codes += item.spbh + ",";
                }
                codes = codes.TrimEnd(',');

                //获取购物单商品关联的所有促销
                List<string> ptList = GetPtIDList(codes);

                if (ptList != null)
                {
                    foreach (string ptID in ptList)
                    {
                        List<Promotion> pt = GetPromotion(ptID);
                        int times;

                        //加判断pt是否为空
                        if (pt != null && pt.Count > 0)
                        {
                            //促销是否有效
                            if (PromotionValid(pt, order[0].Mcard))
                            {

                                switch (pt[0].c_pt_type)
                                {
                                    case "11":
                                        times = GroupSale(order, pt);
                                        benefit += pt[0].n_minus_disc * times;
                                        break;
                                    case "12":
                                        benefit += GroupDiscount(order, pt);
                                        break;
                                    case "13":
                                        times = TwoGroupSale(order, pt);
                                        benefit += pt[0].n_minus_disc * times;
                                        break;
                                    case "14":
                                        benefit += TwoGroupDiscount(order, pt);
                                        break;
                                    case "21":
                                        benefit += GiftSale(order, pt);
                                        break;
                                    case "31":
                                        times = SaleByMoney(order, pt);
                                        benefit += pt[0].n_minus_disc * times;
                                        break;
                                    case "32":
                                        benefit += DiscountByMoney(order, pt);
                                        break;
                                    case "33":
                                        times = SaleByNumber(order, pt);
                                        benefit += pt[0].n_minus_disc * times;
                                        break;
                                    case "61":
                                        benefit += ExtraSale(order, pt);
                                        break;
                                }
                            }


                        }


                    }
                }
            }

            //抹零到角
            return Math.Round(benefit, 1);
        }





        //算出单类商品的优惠
        //处理促销
        //public decimal PromotionProcessor(ref List<OrderItem> order)
        //{

        //    decimal allBenefit = 0;
        //    string codes = string.Empty;

        //    if (order.Count > 0)
        //    {
        //        foreach (OrderItem item in order)
        //        {

        //            decimal benefit = 0;
        //            //先都刷成非促销，再重新计算，以防止先参加促销又删除商品
        //            item.cxbh = "00";
        //            item.cxid = "0";
        //            item.yhmx = 0;

        //            //只查询有效的商品
        //            if (item.isValid)
        //                codes = item.spbh;



        //        //获取购物单商品关联的所有促销
        //        List<string> ptList = GetPtIDList(codes);

        //        if (ptList != null)
        //        {
        //            foreach (string ptID in ptList)
        //            {
        //                List<Promotion> pt = GetPromotion(ptID);
        //                int times;

        //                //促销是否有效
        //                if (PromotionValid(pt, order[0].Mcard))
        //                {
        //                    switch (pt[0].c_pt_type)
        //                    {
        //                        case "11":
        //                            times = GroupSale(order, pt);
        //                            benefit = pt[0].n_minus_disc * times;
        //                            break;
        //                        case "12":
        //                            benefit = GroupDiscount(order, pt);
        //                            break;
        //                        case "13":
        //                            times = TwoGroupSale(order, pt);
        //                            benefit = pt[0].n_minus_disc * times;
        //                            break;
        //                        case "14":
        //                            benefit = TwoGroupDiscount(order, pt);
        //                            break;
        //                        case "21":
        //                            benefit = GiftSale(order, pt);
        //                            break;
        //                        case "31":
        //                            times = SaleByMoney(order, pt);
        //                            benefit = pt[0].n_minus_disc * times;
        //                            break;
        //                        case "32":
        //                            benefit = DiscountByMoney(order, pt);
        //                            break;
        //                        case "33":
        //                            times = SaleByNumber(order, pt);
        //                            benefit = pt[0].n_minus_disc * times;
        //                            break;
        //                        case "61":
        //                            benefit = ExtraSale(order, pt);
        //                            break;
        //                    }
        //                }
        //            }
        //        }

        //            //商品小计要减去优惠的钱数
        //        item.XJ -= benefit;
        //        allBenefit += benefit;
        //    }
        //    }

        //    //抹零到角
        //    return Math.Round(allBenefit, 1);
        //}


        /// <summary>
        /// 根据 order 中商品的货号，获取相关的促销编号
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public List<string> GetPtIDList(string codes)
        {
            return new PromotionDAL().GetPtIDList(codes);
        }

        /// <summary>
        /// 根据促销编号获取促销实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Promotion> GetPromotion(string id)
        {
            return new PromotionDAL().GetPromotion(id);
        }

        /// <summary>
        /// 促销是否有效：时间，周次，客户对象
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        private bool PromotionValid(List<Promotion> pt, string mcard)
        {
            bool IsValid = true;

            // MessageBox.Show("赋值"+IsValid.ToString());
            #region 时间
            #endregion

            #region 周次

            char[] weekdays = pt[0].c_circle.ToCharArray();
            // MessageBox.Show(weekdays.Length.ToString());
            int day = Convert.ToInt32(DateTime.Now.DayOfWeek);
            // MessageBox.Show(day.ToString());
            //判断对应的天是否参加促销
            if (day > 0 && day < 7)
            {
                if (weekdays.Length == 7 && weekdays[day] == '0')
                    return false;
            }

            #endregion
            // MessageBox.Show("方法执行完");
            #region 客户对象
            //不是会员，但促销只针对会员
            if (mcard == "" && pt[0].c_pt_cust == "2")
                return false;
            #endregion
            // MessageBox.Show("方法执行后" + IsValid.ToString());
            return IsValid;
        }

        #region 促销逻辑
        /// <summary>
        /// 11：单品组合减价，返回最后优惠次数
        /// </summary>
        /// <param name="order"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        public int GroupSale(List<OrderItem> order, List<Promotion> pt)
        {
            int times = 0;

            //促销单的商品，在购物单中出现的次数（使用外连接：没出现过，次数就是 0）
            //购物单做了删除操作后（本来在促销单的商品，在购物单中有了负记录，就不会出现空的情况），要先过滤掉 order 中无效的商品
            var ptSale =
                from pi in pt
                join oi in order.Where(o => o.isValid == true)
                on pi.c_sku_id equals oi.spbh into joined
                from j in joined.DefaultIfEmpty(new OrderItem { JYSL = 0 })
                select new { pi.c_sku_id, t = j.JYSL / pi.n_pt_qty };

            times = (int)ptSale.Where(p => p.t > 0).Min(p => p.t);

            if (times > 0)
            {
                //优惠次数
                int count = (int)pt[0].n_pt_count;

                //如果优惠次数大于0，返回优惠次数和倍数中较小的那个
                //如果优惠次数等于0，就是不限制
                if (count > 0)
                    times = Math.Min(count, times);

                //向购物单添加促销数据
                foreach (var p in ptSale)
                {
                    order.Where(o => o.spbh == p.c_sku_id && o.isValid == true).ToList().ForEach(o =>
                    {
                        o.cxid = pt[0].c_pt_id;
                        o.cxbh = pt[0].c_pt_type;
                        o.yhmx = pt[0].n_minus_disc * times;
                    });
                }
            }

            return times;
        }

        /// <summary>
        /// 12：单品组合折扣，返回打折优惠的金额
        /// </summary>
        /// <param name="order"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        public decimal GroupDiscount(List<OrderItem> order, List<Promotion> pt)
        {
            int times = 0;

            //促销单的商品，在购物单中出现的次数（使用外连接：没出现过，次数就是 0）
            //购物单做了删除操作后（本来在促销单的商品，在购物单中有了负记录，就不会出现空的情况），要先过滤掉 order 中无效的商品
            var ptSale =
                from pi in pt
                join oi in order.Where(o => o.isValid == true)
                on pi.c_sku_id equals oi.spbh into joined
                from j in joined.DefaultIfEmpty(new OrderItem { JYSL = 0 })
                select new { pi.c_sku_id, t = j.JYSL / pi.n_pt_qty };

            times = (int)ptSale.Min(p => p.t);

            if (times > 0)
            {
                //优惠次数
                int count = (int)pt[0].n_pt_count;

                //如果优惠次数大于0，返回优惠次数和倍数中较小的那个
                if (count > 0)
                    times = Math.Min(count, times);

                //满足一次促销，购物单中参加促销的每个商品实际优惠的价格
                var ptBenifit =
                    from pi in pt
                    join oi in order.Where(o => o.isValid == true)
                    on pi.c_sku_id equals oi.spbh
                    //应该乘以商品的单价而不是小计
                    select new { oi.spbh, pb = pi.n_minus_disc * pi.n_pt_qty * oi.JYJ };
                // select new { oi.spbh, pb = pi.n_minus_disc * pi.n_pt_qty * oi.XJ };

                decimal benifits = Math.Round(times * ptBenifit.Sum(b => b.pb), 2);

                //向购物单添加促销数据
                foreach (var p in ptBenifit)
                {

                    order.Where(o => o.spbh == p.spbh && o.isValid == true).ToList().ForEach(o =>
                    {
                        o.cxid = pt[0].c_pt_id;
                        o.cxbh = pt[0].c_pt_type;
                        o.yhmx = benifits;
                    });
                }

                //返回总的优惠价格
                return benifits;
            }
            else
                return 0;
        }

        /// <summary>
        /// 13：群组组合减价，返回最后优惠次数
        /// </summary>
        /// <param name="order"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        public int TwoGroupSale(List<OrderItem> order, List<Promotion> pt)
        {
            //从购物单中查询A群的商品
            var aGroup =
                from oi in order
                join pi in pt
                on oi.spbh equals pi.c_sku_id
                where (oi.isValid == true && oi.JYSL >= pi.n_pt_qty && pi.c_plu_ab == "A")
                select new { t = oi.JYSL / pi.n_pt_qty, pi.c_sku_id };
            //得到购物单中A群元素的总数
            var sum_at = (int)aGroup.Sum(a => a.t);

            //从购物单中查询B群的商品
            var bGroup =
                from oi in order
                join pi in pt
                on oi.spbh equals pi.c_sku_id
                where (oi.isValid == true && oi.JYSL >= pi.n_pt_qty && pi.c_plu_ab == "B")
                select new { t = oi.JYSL / pi.n_pt_qty, pi.c_sku_id };
            var sum_bt = (int)bGroup.Sum(b => b.t);

            int times = Math.Min(sum_at, sum_bt);

            //优惠次数
            int count = (int)pt[0].n_pt_count;

            //如果优惠次数大于0，返回优惠次数和倍数中较小的那个
            //如果优惠次数等于0，就是不限制，返回倍数
            if (count > 0)
                times = Math.Min(count, times);

            if (times > 0)
            {
                var ptSales =
                    from oi in order
                    join pi in pt
                    on oi.spbh equals pi.c_sku_id
                    where (oi.isValid == true && oi.JYSL >= pi.n_pt_qty)
                    select new { oi.spbh };

                //向购物单添加促销数据
                foreach (var p in ptSales)
                {
                    order.Where(o => o.spbh == p.spbh && o.isValid == true).ToList().ForEach(o =>
                    {
                        o.cxid = pt[0].c_pt_id;
                        o.cxbh = pt[0].c_pt_type;
                        o.yhmx = pt[0].n_minus_disc * times;
                    });
                }
            }

            return times;
        }

        /// <summary>
        /// 14：群组组合折扣
        /// </summary>
        /// <param name="order"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        public decimal TwoGroupDiscount(List<OrderItem> order, List<Promotion> pt)
        {
            decimal toDiscount = 0;
            decimal benefits = 0;

            //从购物单中查询A群的商品
            var aGroup =
                from oi in order
                join pi in pt
                on oi.spbh equals pi.c_sku_id
                where (oi.isValid == true && oi.JYSL >= pi.n_pt_qty && pi.c_plu_ab == "A")
                select new GroupDiscountItem { spbh = oi.spbh, qty = (int)pi.n_pt_qty, money = oi.JYJ * pi.n_pt_qty, t = (int)(oi.JYSL / pi.n_pt_qty) };

            //从购物单中查询B群的商品
            var bGroup =
                from oi in order
                join pi in pt
                on oi.spbh equals pi.c_sku_id
                where (oi.isValid == true && oi.JYSL >= pi.n_pt_qty && pi.c_plu_ab == "B")
                select new GroupDiscountItem { spbh = oi.spbh, qty = (int)pi.n_pt_qty, money = oi.JYJ * pi.n_pt_qty, t = (int)(oi.JYSL / pi.n_pt_qty) };

            if (aGroup.Count() > 0 && bGroup.Count() > 0)
            {
                //组合打折的倍数
                int time;

                //实际打折次数
                int countRecord = 0;
                //促销规定的可打折次数（优惠次数）
                int count = (int)pt[0].n_pt_count;

                //实际参与打折商品的数量
                //Dictionary<string, int> itemDiscount = new Dictionary<string, int>();

                //得到查询结果，按商品指定购买数量对应的总金额升序排列
                List<GroupDiscountItem> ag = aGroup.OrderBy(a => a.money).ToList();
                List<GroupDiscountItem> bg = bGroup.OrderBy(b => b.money).ToList();

                //根据金额，从低到高计算打折
                foreach (var i in ag)
                {
                    foreach (var j in bg)
                    {
                        //count 为 0，打折次数不限；
                        //count 不为 0，就是实际可以打折的最大次数
                        if (count == 0 || (countRecord < count))
                        {
                            //打折次数为两个商品参与打折次数的最小值
                            time = Math.Min(i.t, j.t);
                            if (time > 0)
                            {
                                //计算打折的金额
                                toDiscount += time * (i.money + j.money);
                                //去掉已打折
                                i.t -= time;
                                j.t -= time;

                                //打折次数加1
                                countRecord++;

                                ////记录打折商品的数量
                                //if (itemDiscount.ContainsKey(i.spbh))
                                //    itemDiscount[i.spbh] += time * i.qty;
                                //else
                                //    itemDiscount.Add(i.spbh, time * i.qty);

                                //if (itemDiscount.ContainsKey(j.spbh))
                                //    itemDiscount[j.spbh] += time * i.qty;
                                //else
                                //    itemDiscount.Add(j.spbh, time * i.qty);
                            }
                        }
                    }
                }

                if (countRecord > 0)
                {
                    benefits = Math.Round(toDiscount * pt[0].n_minus_disc, 2);

                    var ptSales =
                        from oi in order
                        join pi in pt
                        on oi.spbh equals pi.c_sku_id
                        where (oi.isValid == true && oi.JYSL >= pi.n_pt_qty)
                        select new { oi.spbh };
                    //向购物单添加促销数据
                    foreach (var p in ptSales)
                    {
                        order.Where(o => o.spbh == p.spbh && o.isValid == true).ToList().ForEach(o =>
                        {
                            o.cxid = pt[0].c_pt_id;
                            o.cxbh = pt[0].c_pt_type;
                            o.yhmx = benefits;
                        });
                    }
                }
            }

            return benefits;
        }

        /// <summary>
        /// 21：搭赠，返回搭赠总值
        /// </summary>
        /// <param name="order"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        public decimal GiftSale(List<OrderItem> order, List<Promotion> pt)
        {
            decimal gift = 0;
            //从购物单中查询商品A
            var aGroup =
                from oi in order
                join pi in pt
                on oi.spbh equals pi.c_sku_id
                where (oi.isValid == true && oi.JYSL >= pi.n_pt_qty && pi.c_plu_ab == "A")
                select new { oi.spbh, t = (int)(oi.JYSL / pi.n_pt_qty) };

            //从购物单中查询商品B
            var bGroup =
                from oi in order
                join pi in pt
                on oi.spbh equals pi.c_sku_id
                where (oi.isValid == true && oi.JYSL >= pi.n_pt_qty && pi.c_plu_ab == "B")
                select new { oi.spbh, money = pi.n_pt_qty * oi.JYJ, t = (int)(oi.JYSL / pi.n_pt_qty) };

            if (aGroup.Count() > 0 && bGroup.Count() > 0)
            {
                int count = (int)pt[0].n_pt_count;
                if (count != 0)
                    gift = Math.Min(count, Math.Min(aGroup.First().t, bGroup.First().t))
                        * bGroup.First().money;
                else
                    gift = Math.Min(aGroup.First().t, bGroup.First().t) * bGroup.First().money;

                var ptSales =
                    from oi in order
                    join pi in pt
                    on oi.spbh equals pi.c_sku_id
                    where (oi.isValid == true && oi.JYSL >= pi.n_pt_qty)
                    select new { oi.spbh };
                //向购物单添加促销数据
                foreach (var p in ptSales)
                {
                    order.Where(o => o.spbh == p.spbh && o.isValid == true).ToList().ForEach(o =>
                    {
                        o.cxid = pt[0].c_pt_id;
                        o.cxbh = pt[0].c_pt_type;
                        o.yhmx = gift;
                    });
                }
            }

            return gift;
        }

        /// <summary>
        /// 31：买每满额 X 元减价 Y 元
        /// </summary>
        /// <param name="order"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        public int SaleByMoney(List<OrderItem> order, List<Promotion> pt)
        {
            int times = 0;

            var ptSale =
                from pi in pt
                join oi in order.Where(o => o.isValid == true)
                on pi.c_sku_id equals oi.spbh
                select new { oi.spbh, money = oi.JYSL * oi.JYJ };

            //到底满几次
            if (ptSale.Count() > 0)
            {
                try
                {
                    times = (int)(ptSale.Sum(s => s.money) / pt[0].n_cond_amt);
                }
                catch { }
            }
            if (times > 0)
            {
                int count = (int)pt[0].n_pt_count;

                //如果优惠次数大于0，返回优惠次数和倍数中较小的那个
                //如果优惠次数等于0，就是不限制，返回倍数
                if (count > 0)
                    times = Math.Min(count, times);

                //向购物单添加促销数据
                foreach (var p in ptSale)
                {
                    order.Where(o => o.spbh == p.spbh && o.isValid == true).ToList().ForEach(o =>
                    {
                        o.cxid = pt[0].c_pt_id;
                        o.cxbh = pt[0].c_pt_type;
                        o.yhmx = pt[0].n_minus_disc * times;
                    });
                }
            }
            return times;
        }

        /// <summary>
        /// 32：买满额 X 元以上折扣 Y%
        /// </summary>
        /// <param name="order"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        public decimal DiscountByMoney(List<OrderItem> order, List<Promotion> pt)
        {
            decimal benefits = 0;

            //在购物单中查询参加促销的商品，返回商品
            var ptSale =
                from pi in pt
                join oi in order.Where(o => o.isValid == true)
                on pi.c_sku_id equals oi.spbh
                select new { oi.spbh, money = oi.JYSL * oi.JYJ, toDiscount = (pi.n_pt_qty >= oi.JYSL ? pi.n_pt_qty : oi.JYSL) * oi.JYJ };

            if (ptSale.Count() > 0)
            {
                //计算购物单中参加促销商品的总金额
                decimal sum = ptSale.Sum(s => s.money);
                //是否达到优惠条件
                if (sum > pt[0].n_cond_amt)
                {
                    //计算优惠
                    benefits = ptSale.Sum(s => s.money) * pt[0].n_minus_disc;
                    ////计算优惠：toDiscount 受促销商品参与促销的最大笔数限制
                    //benefits = ptSale.Sum(s=>s.toDiscount) * pt[0].n_minus_disc;

                    //向购物单添加促销数据
                    foreach (var p in ptSale)
                    {
                        order.Where(o => o.spbh == p.spbh && o.isValid == true).ToList().ForEach(o =>
                        {
                            o.cxid = pt[0].c_pt_id;
                            o.cxbh = pt[0].c_pt_type;
                            o.yhmx = Math.Round(benefits, 2);
                        });
                    }
                }
            }

            return benefits;
        }

        /// <summary>
        /// 33：买满 X 个减价 Y 元
        /// </summary>
        /// <param name="order"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        public int SaleByNumber(List<OrderItem> order, List<Promotion> pt)
        {
            int times = 0;

            //在购物单中查询参加促销的商品
            var ptSale =
                from pi in pt
                join oi in order.Where(o => o.isValid == true)
                on pi.c_sku_id equals oi.spbh
                select new { oi.spbh, oi.JYSL, oi.number };

            //到底满几次
            if (ptSale.Count() > 0)
            {
                try
                {
                    times = (int)(ptSale.Sum(s => (s.JYSL * s.number)) / pt[0].n_cond_amt);
                }
                catch
                {
                }
            }
            if (times > 0)
            {
                int count = (int)pt[0].n_pt_count;

                //如果优惠次数大于0，返回优惠次数和倍数中较小的那个
                //如果优惠次数等于0，就是不限制，返回倍数
                if (count > 0)
                    times = Math.Min(count, times);

                //向购物单添加促销数据
                foreach (var p in ptSale)
                {
                    order.Where(o => o.spbh == p.spbh && o.isValid == true).ToList().ForEach(o =>
                    {
                        o.cxid = pt[0].c_pt_id;
                        o.cxbh = pt[0].c_pt_type;
                        o.yhmx = pt[0].n_minus_disc * times;
                    });
                }
            }
            return times;
        }

        /// <summary>
        /// 61：加购价
        /// </summary>
        /// <param name="order"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        public decimal ExtraSale(List<OrderItem> order, List<Promotion> pt)
        {
            decimal benifits = 0;

            //从购物单中查询A群的商品
            var aGroup =
                from oi in order
                join pi in pt
                on oi.spbh equals pi.c_sku_id
                where (oi.isValid == true && oi.JYSL >= pi.n_pt_qty && pi.c_plu_ab == "A")
                select new { oi.spbh, money = oi.JYSL * oi.JYJ };

            //从购物单中查询商品B
            var bGroup =
                from oi in order
                join pi in pt
                on oi.spbh equals pi.c_sku_id
                where (oi.isValid == true && oi.JYSL >= pi.n_pt_qty && pi.c_plu_ab == "B")
                select new { oi.spbh, oi.JYSL, b = oi.JYJ - pi.n_minus_disc };

            if (aGroup.Count() > 0 && bGroup.Count() > 0)
            {
                int times = 0;
                int extra = 0;

                //可以加购几次
                try
                {
                    ////测试
                    //int cc = (int)(aGroup.Sum(a => (a.money)));
                    //MessageBox.Show(cc.ToString());

                    times = (int)(aGroup.Sum(a => (a.money)) / pt[0].n_cond_amt);
                }
                catch
                {

                }
                //商品B的实际数量
                extra = (int)bGroup.First().JYSL;
                //实际可以加购的数量
                times = Math.Min(times, extra);
                if (times > 0)
                {
                    int count = (int)pt[0].n_pt_count;
                    if (count != 0)
                        benifits = Math.Min(count, times) * bGroup.First().b;
                    else
                        benifits = times * bGroup.First().b;

                    //查询购物单中参加促销的商品
                    var ptSale =
                        from oi in order
                        join pi in pt
                        on oi.spbh equals pi.c_sku_id
                        where (oi.isValid == true && oi.JYSL >= pi.n_pt_qty)
                        select new { oi.spbh };
                    //向购物单添加促销数据
                    foreach (var p in ptSale)
                    {
                        order.Where(o => o.spbh == p.spbh && o.isValid == true).ToList().ForEach(o =>
                        {
                            o.cxid = pt[0].c_pt_id;
                            o.cxbh = pt[0].c_pt_type;
                            o.yhmx = benifits;
                        });
                    }
                }
            }
            return benifits;
        }












        /// <summary>
        /// XX：搭赠，返回搭赠总值
        /// </summary>
        /// <param name="order"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        public decimal mixSale(List<OrderItem> order, List<Promotion> pt)
        {
            decimal gift = 0;
            //从购物单中查询商品A
            var aGroup =
                from oi in order
                join pi in pt
                on oi.spbh equals pi.c_sku_id
                where (oi.isValid == true && oi.JYSL >= pi.n_pt_qty && pi.c_plu_ab == "A")
                select new { num = oi.JYJ, oi.spbh, t = (int)(oi.JYSL / pi.n_pt_qty) };



            if (aGroup.Count() > 0)
            {
                int count = (int)pt[0].n_pt_count;
                if (count != 0)
                    gift = Math.Min(count, aGroup.First().t) * aGroup.First().num;
                else
                    gift = aGroup.First().t * aGroup.First().num;

                var ptSales =
                    from oi in order
                    join pi in pt
                    on oi.spbh equals pi.c_sku_id
                    where (oi.isValid == true && oi.JYSL >= pi.n_pt_qty)
                    select new { oi.spbh };
                //向购物单添加促销数据
                foreach (var p in ptSales)
                {
                    order.Where(o => o.spbh == p.spbh).ToList().ForEach(o =>
                    {
                        o.cxid = pt[0].c_pt_id;
                        o.cxbh = pt[0].c_pt_type;
                        o.yhmx = gift;
                    });
                }
            }

            return gift;
        }

















        #endregion

        /// <summary>
        /// 清空促销
        /// </summary>
        /// <returns></returns>
        public int DeletePromotion()
        {
            return new PromotionDAL().DeletePromotion();
        }
    }

    /// <summary>
    /// 群组组合折扣的数据项
    /// </summary>
    public class GroupDiscountItem
    {
        /// <summary>
        /// 打折商品的货号
        /// </summary>
        public string spbh { get; set; }

        /// <summary>
        /// 打折商品的指定购买数量
        /// </summary>
        public int qty;

        /// <summary>
        /// 打折商品指定数量时的总金额
        /// </summary>
        public decimal money { get; set; }

        /// <summary>
        /// 商品实际可以参与打折的次数（交易数量/指定数量）
        /// </summary>
        public int t { get; set; }
    }

}
