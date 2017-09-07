using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace POSApplication.Common
{
    public class ObjFactory
    {
        /// <summary>
        /// 创建顾客显示牌对象实例
        /// </summary>
        /// <returns></returns>
        public static ICustomerDisplayer CreateCustomerDisplayer()
        {
            string typeName = ConfigurationManager.AppSettings["CustomerDisplayerType"];
            switch (typeName)
            {
                case "TECT10": return new POSApplication.Common.CustomerDisplay();
                case "X80": return new POSApplication.Common.CustomerDisplayerX80();
                default: return new POSApplication.Common.CustomerDisplay();
            }

            //Assembly ass = Assembly.GetExecutingAssembly();
            //return (ICustomerDisplayer)ass.CreateInstance(typeName, true);
        }
    }
}
