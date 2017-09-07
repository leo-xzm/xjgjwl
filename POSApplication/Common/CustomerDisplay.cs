using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace POSApplication.Common
{
    public class CustomerDisplay : POSApplication.Common.ICustomerDisplayer
    {
        public SerialPort InitSettings
        {
            get
            {
                SerialPort serialPort = new SerialPort(ConfigHelper.GetAppConfig("KXP_Com"));
                serialPort.BaudRate = 115200;
                serialPort.Parity = Parity.Odd;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;
                return serialPort;
            }
        }

        /// <summary>
        /// 向客显发送显示内容
        /// </summary>
        /// <param name="com"></param>
        /// <param name="content"></param>
        public void SendToDisplay(SerialPort com, string content)
        {
            char[] charArray = content.ToCharArray();
            Byte[] bArray = new Byte[500];

            for (int i = 0; i < charArray.Length; i++)
            {
                int j = Convert.ToInt32(charArray[i]);

                bArray[i] = Convert.ToByte(j);
            }

            if (!com.IsOpen)
                com.Open();
            com.Write(bArray, 0, content.Length);
        }

        /// <summary>
        /// 客显清屏 ECS [ 2 J --- 1B 5B 32 4A
        /// </summary>
        public void CLS(SerialPort com)
        {
            if (!com.IsOpen)
                com.Open();

            byte[] bt = new byte[] { 27, 91, 50, 74 };
            com.Write(bt, 0, 4);
        }

        public string GetTypeID()
        {
            return "TECT10";
        }


        public void Display(SerialPort com, CustomerDisplayData data)
        {
            string str = string.Format(" Money: {0}" + Environment.NewLine
                           + "   Pay: {1}" + Environment.NewLine
                           + "Change: {2}", data.Total, data.Pay, data.Change);

            CLS(com);
            SendToDisplay(com, str);
        }
    }
}
