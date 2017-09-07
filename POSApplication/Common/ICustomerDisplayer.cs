using System;
namespace POSApplication.Common
{
    public interface ICustomerDisplayer
    {
        string GetTypeID();
        System.IO.Ports.SerialPort InitSettings { get; }
        void CLS(System.IO.Ports.SerialPort com);
        void SendToDisplay(System.IO.Ports.SerialPort com, string content);
        /// <summary>
        /// 显示数据
        /// </summary>
        void Display(System.IO.Ports.SerialPort com, CustomerDisplayData data);
    }
}
