using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using POSApplication.UI;

namespace POSApplication
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogIn());
            
            //Application.Run(new frmPay());
            //Application.Run(new frmRefund());
            //Application.Run(new frmReceipt());
            //Application.Run(new frmRefundPow());
            //Application.Run(new frmMain());
        }
    }
}
