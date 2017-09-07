using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace POSApplication.Common
{
    /// <summary>
    /// 生e通 X80 POS机搭配的顾客显示牌
    /// </summary>
    public class CustomerDisplayerX80 : ICustomerDisplayer
    {
        public System.IO.Ports.SerialPort InitSettings
        {
            get
            {
                SerialPort serialPort = new SerialPort(ConfigHelper.GetAppConfig("KXP_Com"));
                serialPort.BaudRate = 2400;
                serialPort.Parity = Parity.None;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;
                return serialPort;
            }
        }

        /// <summary>
        /// 清屏 [0CH]
        /// </summary>
        /// <param name="com"></param>
        public void CLS(System.IO.Ports.SerialPort com)
        {
            //SendToDisplay(com, "0C");
            if (!com.IsOpen)
                com.Open();
            //char[] array = "0C".ToCharArray();
            //foreach (char letter in array)
            //{
            //    int value = Convert.ToInt32(letter);
            //    string hexOutput = String.Format("{0:X}", value);
            //    letter = hexOutput.ToString();
            //    Console.WriteLine("Hex value of {0} is {1}", letter, hexOutput);
            //}
            //com.Write("0C".ToCharArray(), 0, 2);
            byte[] arr = { 0, Convert.ToByte("C", 16) };
            com.Write(arr, 0, arr.Length);
        }

        public void SendToDisplay(System.IO.Ports.SerialPort com, string content)
        {
            //content = "12.31";

            string[] strArr = content.Split('.');

            //拆分成2个字符串
            string str1 = strArr[0];
            string str2 = strArr[1];

            //2个字符串长度
            int len1 = str1.Length;
            int len2 = str2.Length;

            //两个字符数组
            char[] charArr1 = str1.ToCharArray();
            char[] charArr2 = str2.ToCharArray();

            string[] strArr1 = new string[2 * str1.Length];
            string[] strArr2 = new string[2 * str2.Length];

            //新的字符串的长度
            int len = content.Length;

            //创建一个十六进制显示的字符串数组
            string[] array = new string[len];

            for (int i = 0; i < len1; i++)
            {
                array[i] = "3" + charArr1[i].ToString();
            }

            array[len1] = "2E";

            for (int i = 0; i < len2; i++)
            {
                array[len1 + 1 + i] = "3" + charArr2[i];
            }

            string fina = string.Join("", array);

            byte[] byteArr = strToToHexByte(fina);


            if (!com.IsOpen)
                com.Open();
            com.Write(byteArr, 0, byteArr.Length);
        }
        //将字符串转换为16进制的字符数组
        private byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public string GetTypeID()
        {
            return "X80";
        }

        public void Display(SerialPort com, CustomerDisplayData data)
        {
            string str = "";
            switch (data.StatusLight)
            {
                case 1: str = data.Price; break;
                case 2: str = data.Total; break;
                case 3: str = data.Pay; break;
                case 4: str = data.Change; break;
            }

            CLS(com);
            byte[] status = strToToHexByte("1B733" + data.StatusLight);
            //状态灯
            com.Write(status, 0, status.Length);
            //SendToDisplay(com, "1B7331");//状态灯

            //com.Write("1B7331".ToCharArray(), 0, 6);
            SendToDisplay(com, str);//数据
        }
    }
}