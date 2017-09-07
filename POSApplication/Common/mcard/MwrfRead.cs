using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace POSApplication.Common
{

    public class MwrfRead
    {
        public int icdev; // 通讯设备标识符
        public Int16 st;
        public int sec;

        /// <summary>
        /// 读取会员卡
        /// </summary>
        /// <param name="skey">卡密</param>
        /// <param name="Sec">扇区</param>
        public string ReadCardID(string skey, string Sec)
        {
            //释放设备
            MwrfCommon.rf_exit(icdev);
            //初始化设备
            InitRfDev();
            //寻卡
            SeekCard();
            //卡认证
            AuthCard(skey, Sec);
            //读取数据
            string readData = ReadData();
            return readData;
        }

        /// <summary>
        /// 读卡器声音
        /// </summary>
        public void RFbeep()
        {
            MwrfCommon.rf_beep(icdev, 10);
        }

        private string ReadData()
        {
            int i = 0;
            byte[] data = new byte[32];
            //byte[] buff = new byte[32];

            for (i = 0; i < 32; i++)
                data[i] = 0;
            //for (i = 0; i < 32; i++)
            //    buff[i] = 0;

            //读取指定扇区 sec 数据
            st = mifareone.rf_read_hex(icdev, sec, data);
            if (st == 0)
            {
                //MwrfCommon.hex_a(data, buff, 16);
                //string Date = System.Text.Encoding.ASCII.GetString(buff);
                string Date = System.Text.Encoding.ASCII.GetString(data).ToLower();
                return Date;
            }
            return "";
        }

        private void AuthCard(string skey, string Sec)
        {
            byte[] key1 = new byte[17];
            byte[] key2 = new byte[7];
            int i = 0;
            int keylen = skey.Length;
            if (keylen != 12)
            {
                //MessageBox.Show("请正确输入密码，密码长度不对！");
                return;
            }
            if (Sec.Length < 1)
            {
                //MessageBox.Show("扇区号不正确！");
                return;
            }

            sec = Convert.ToInt32(Sec, 10);
            if (sec < 0 || sec > 15)
            {
                //MessageBox.Show("扇区号不正确！");
                return;
            }

            for (i = 0; i < keylen; i++)
            {
                if (skey[i] >= '0' && skey[i] <= '9')
                    continue;
                if (skey[i] <= 'a' && skey[i] <= 'f')
                    continue;
                if (skey[i] <= 'A' && skey[i] <= 'F')
                    continue;
            }
            if (i != keylen)
            {
                MessageBox.Show("密码必须为十六进制数！");
                return;
            }
            key1 = Encoding.ASCII.GetBytes(skey);
            MwrfCommon.a_hex(key1, key2, 12);
            st = MwrfCommon.rf_load_key(icdev, 0, sec, key2);
            if (st != 0)
            {
                //MessageBox.Show("装载密码失败！");
                return;
            }
            st = mifareone.rf_authentication(icdev, 0, sec);
            //if (st != 0)
            //MessageBox.Show("认证失败！");
        }

        private void SeekCard()
        {
            UInt16 tagtype = 0;
            byte size = 0;
            uint snr = 0;

            mifareone.rf_reset(icdev, 3);
            st = mifareone.rf_request(icdev, 1, out tagtype);
            if (st != 0)
            {
                //MessageBox.Show("寻卡出错！");
                return;
            }

            st = mifareone.rf_anticoll(icdev, 0, out snr);
            if (st != 0)
            {
                //MessageBox.Show("寻卡出错！");
                return;
            }
            string snrstr = "";
            snrstr = snr.ToString("X");

            st = mifareone.rf_select(icdev, snr, out size);
            if (st != 0)
            {
                //MessageBox.Show("寻卡出错！");
                return;
            }
        }

        private void InitRfDev()
        {
            try
            {
                st = 0;
                byte[] ver = new byte[30];
                st = MwrfCommon.lib_ver(ver);
                string sver = System.Text.Encoding.ASCII.GetString(ver);
                Int16 port = 0;
                int baud = 115200;
                icdev = MwrfCommon.rf_init(port, baud);
                if (icdev > 0)
                {
                    byte[] status = new byte[30];
                    st = MwrfCommon.rf_get_status(icdev, status);
                    // common.rf_beep(icdev, 5);
                }
                //else
                //MessageBox.Show("打开串口失败！");
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show("打开串口失败！");
                throw ex;
            }
        }

    }
}
