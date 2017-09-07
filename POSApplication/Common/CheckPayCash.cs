using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POSApplication.Common
{
    public class CheckPayCash
    {
        /// <summary>
        /// 实收金额是否合理
        /// </summary>
        /// <param name="hj">应收现金</param>
        /// <param name="zl">找零</param>
        /// <returns></returns>
        public static bool Check(decimal hj, decimal zl)
        {
            bool ret = true;

            //实收现金
            decimal ss = hj + zl;

            int s100, s50, s20, s10, s5, s1, z100, z50, z20, z10, z5, z1, t;

            if (zl >= 0 && zl < 100)
            {
                //实收中100的数量
                s100 = (int)(ss / 100);
                t = s100 * 100;
                //实收中50的数量
                s50 = (int)((ss - t) / 50);
                t += s50 * 50;
                //实收中20的数量
                s20 = (int)((ss - t) / 20);
                t += s20 * 20;
                //实收中10的数量
                s10 = (int)((ss - t) / 10);
                t += s10 * 10;
                //实收中5的数量
                s5 = (int)((ss - t) / 5);
                t += s5 * 5;
                //实收中1的数量
                s1 = (int)((ss - t) / 1);

                //找零中100的数量
                z100 = (int)(zl / 100);
                t = z100 * 100;
                //实收中50的数量
                z50 = (int)((zl - t) / 50);
                t += z50 * 50;
                //实收中20的数量
                z20 = (int)((zl - t) / 20);
                t += z20 * 20;
                //实收中10的数量
                z10 = (int)((zl - t) / 10);
                t += z10 * 10;
                //实收中5的数量
                z5 = (int)((zl - t) / 5);
                t += z5 * 5;
                //实收中1的数量
                z1 = (int)((zl - t) / 1);

                //实收和找零出现相同的票面就不合理了
                if (z100 > 0)
                    ret = false;
                if (s50 > 0 && z50 > 0)
                    ret = false;
                if (s20 > 0 && z20 > 0)
                    ret = false;
                if (s10 > 0 && z10 > 0)
                    ret = false;
                if (s5 > 0 && z5 > 0)
                    ret = false;
                if (s1 > 0 && z1 > 0)
                    ret = false;
            }
            else
                ret = false;

            return ret;
        }
    }
}
