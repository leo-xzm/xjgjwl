using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POSApplication.Model;
using POSApplication.DAL;

namespace POSApplication.BLL
{
    public class SalesBLL
    {
        /// <summary>
        /// 结账时，新增单次商品销售明细数据
        /// </summary>
        /// <param name="salesList"></param>
        /// <returns></returns>
        public bool AddSalesTime(List<Sales> salesList)
        {
            bool result = true;

            foreach (Sales sales in salesList)
            {
                if (new SalesDAL().AddSalesTime(sales) < 0)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 清空单次商品销售明细数据
        /// </summary>
        /// <returns></returns>
        public int DeleteSalesTime()
        {
            return new SalesDAL().DeleteSalesTime();
        }

        /// <summary>
        /// 删除单次销售明细表中的999校验
        /// </summary>
        /// <returns></returns>
        public int DeleteSalesTime999()
        {
            return new SalesDAL().DeleteSalesTime999();
        }

        /// <summary>
        /// 清空单日销售明细表
        /// </summary>
        /// <returns></returns>
        public int DeleteSalesDay()
        {
            return new SalesDAL().DeleteSalesDay();
        }

        /// <summary>
        /// 新增单日商品销售明细数据
        /// </summary>
        /// <returns></returns>
        public bool AddSalesDay()
        {
            return new SalesDAL().AddSalesDay();
        }

        /// <summary>
        /// 新增商品销售明细数据
        /// </summary>
        /// <returns></returns>
        public bool AddSalesData()
        {
            return new SalesDAL().AddSalesData();
        }

        /// <summary>
        /// 查询 sales_backups 表数据量
        /// </summary>
        /// <returns></returns>
        public int SelectSalesBackups()
        {
            return new SalesDAL().SelectSalesBackups();
        }

        /// <summary>
        /// 查询 sales_unsure 表数据量
        /// </summary>
        /// <returns></returns>
        public int SelectSalesUnsure()
        {
            return new SalesDAL().SelectSalesUnsure();
        }

        /// <summary>
        /// 清空 sales_unsure 表
        /// </summary>
        /// <returns></returns>
        public int DeleteSalesUnsure()
        {
            return new SalesDAL().DeleteSalesUnsure();
        }

        /// <summary>
        /// 追加数据 从 sales_time 到 sales_backups 表
        /// </summary>
        /// <returns></returns>
        public int TimeToBackups()
        {
            return new SalesDAL().TimeToBackups();
        }

        /// <summary>
        /// 追加数据 从 sales_time 到 sales_unsure 表
        /// </summary>
        /// <returns></returns>
        public int TimeToUnsure()
        {
            return new SalesDAL().TimeToUnsure();
        }

        /// <summary>
        /// 追加数据 从 sales_unsure 到 sales_backups 表
        /// </summary>
        /// <returns></returns>
        public int UnsureToBackups()
        {
            return new SalesDAL().UnsureToBackups();
        }

        /// <summary>
        /// 追加数据 从 sales_backups 到 sales_unsure 表
        /// </summary>
        /// <returns></returns>
        public int BackupsToUnsure()
        {
            return new SalesDAL().BackupsToUnsure();
        }

        /// <summary>
        /// 清空 backups，复制数据 从 time 到 backups 表
        /// </summary>
        /// <returns></returns>
        public int TimeCoverBackups()
        {
            return new SalesDAL().TimeCoverBackups();
        }

        /// <summary>
        /// 获取当日销售明细中的支付数据
        /// </summary>
        /// <returns></returns>
        public List<Sales> GetSalesDay()
        {
            return new SalesDAL().GetSalesDay();
        }



        /// <summary>
        /// 根据条码获取商品
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Goods GetGoodsByCode(string code)
        {
            return new SalesDAL().GetGoodsByCode(code);
        }


        /// <summary>
        /// 根据货号获取商品，默认选择规格数量为1，且条形码最小的
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public Goods GetGoodsByCID(string cid)
        {
            return new SalesDAL().GetGoodsByCID(cid);
        }
    }
}
