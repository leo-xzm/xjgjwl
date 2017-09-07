using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POSApplication.Model;
using POSApplication.Common;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace POSApplication.UI
{
    public partial class frmReceipt : Form
    {
        /// <summary>
        /// 收款员
        /// </summary>
        private Staff staff;

        /// <summary>
        /// 主界面
        /// </summary>
        private frmMain main;

        /// <summary>
        /// 补打的小票
        /// </summary>
        private Receipt receipt;
        public List<Receipt> rList { get; set; }


        //OrderName on = new OrderName();
        //public class OrderName
        //{
        //    private String _oldName=null;
        //    private String _newName=null;
        //    public string oldName { get{return this._oldName;} set{this._oldName=value;} }
        //    public string newName { get{return this._newName;} set{this._newName=value;} }
        //    public override string ToString()
        //    {
        //        return this._newName;
        //    }
        //}
        /// <summary>
        /// 小票列表文件
        /// </summary>
        private string receiptFile { get; set; }

        public frmReceipt() {
            InitializeComponent();
            staff = new Staff();
            staff.Staff_name = "ddd";
        }
        /// <summary>
        /// 小票列表文件
        /// </summary>

        public frmReceipt(Staff staff, frmMain main)
        {
            InitializeComponent();

            this.staff = staff;
            this.main = main;
        }

        private void frmReceipt_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lbJH.Text = "机号：" + ConfigHelper.GetAppConfig("JH");
            lbStaff.Text = "员工：" + staff.Staff_name;
            rList = new List<Receipt>();


            //年份下拉框绑定数据
            DirectoryInfo di = new DirectoryInfo(GlobalParams.DataPath);
            DirectoryInfo[] dirs = di.GetDirectories();

            if (dirs.Length > 0)
            {
                //排除suspendorder目录
                for (int i = 0; i < dirs.Length - 1; i++)
                    cbbYear.Items.Add(dirs[i]);

                //cbbYear.SelectedItem = dirs[dirs.Length - 2];
            }


            ////默认打开显示最近一次数据  
            cbbYear.Text = dirs[dirs.Length - 2].ToString();

            DirectoryInfo m = new DirectoryInfo(GlobalParams.DataPath + "\\" + cbbYear.Text);
            DirectoryInfo[] dirsm = m.GetDirectories();
            cbbMonth.Text = dirsm[dirsm.Length - 1].ToString();

            DirectoryInfo d = new DirectoryInfo(GlobalParams.DataPath + "\\" + cbbYear.Text + "\\" + cbbMonth.Text);
            FileInfo[] dirsd = d.GetFiles();

            int a = 0;
            if (dirsd.Length > 0)
            {
                foreach (FileInfo file in dirsd)
                {
                    String exname = file.ToString();
                    String ex = exname.Substring(exname.LastIndexOf(".") + 1);
                    if (ex == "rec")
                        a++;
                }
                cbbDay.Text = dirsd[a - 1].Name.Substring(6, 2);

            }

            dgvReceipt.ColumnHeadersDefaultCellStyle.Font=new Font("宋体",10F);
            dgvReceipt.RowsDefaultCellStyle.ForeColor = SystemColors.WindowText;
            dgvReceipt.RowsDefaultCellStyle.Font = new Font("宋体", 9F);
        }




        private void btnPrint_Click(object sender, EventArgs e)
        {
            //打印小票
            Print prt = new Print();
            prt.rec = receipt;
            prt.PrintReceipt(false);

            Log.WriteNormalLog(string.Format("收银员：{0}补打小票 {1}-{2}",
                staff.Staff_name, receiptFile.Substring(0, 8), receipt.xph), "", "");


           // btnPrint.Enabled = false;   //打印完禁用打印按钮
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            main.Show();
            this.Dispose();
        }

        #region 下拉框选择事件

        private void cbbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbMonth.Text = "";
            cbbMonth.Items.Clear();
            cbbDay.Text = "";
            cbbDay.Items.Clear();
            txtReceipt.Text = "";
            //清空 dgvReceipt
            rList = new List<Receipt>();
            dgvReceipt.DataSource = new BindingList<Receipt>(rList);
            DirectoryInfo di = new DirectoryInfo(GlobalParams.DataPath + "\\" + cbbYear.Items[cbbYear.SelectedIndex]);
            DirectoryInfo[] dirs = di.GetDirectories();

            if (dirs.Length > 0)
            {
                for (int i = 0; i < dirs.Length; i++)
                    cbbMonth.Items.Add(dirs[i]);
            }
            else
            {
                cbbDay.Text = "";
                cbbDay.Items.Clear();
            }
        }

        private void cbbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbDay.Text = "";
            cbbDay.Items.Clear();
            txtReceipt.Text = "";
            //清空 dgvReceipt
            rList = new List<Receipt>();
            dgvReceipt.DataSource = new BindingList<Receipt>(rList);

            string path = GlobalParams.DataPath + "\\" + cbbYear.Items[cbbYear.SelectedIndex] + "\\" + cbbMonth.Items[cbbMonth.SelectedIndex];
            DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles(); // 获取所有文件信息
            if (files.Length > 0)
            {
                foreach (FileInfo file in files)
                {
                    String exname = file.ToString();
                    String ex = exname.Substring(exname.LastIndexOf(".") + 1);
                    if (ex == "rec")
                        cbbDay.Items.Add(file.Name.Substring(6, 2));
                }
            }
        }

        private void cbbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtReceipt.Text = "";
            ShowReceiptList();
        }

        /// <summary>
        /// dgvReceipt 绑定数据并显示
        /// </summary>
        private void ShowReceiptList()
        {
            int year = Convert.ToInt32(cbbYear.SelectedItem.ToString());
            int month = Convert.ToInt32(cbbMonth.SelectedItem.ToString());
            int day = Convert.ToInt32(cbbDay.SelectedItem.ToString());
            DateTime time = new DateTime(year, month, day);
            GlobalParams.DayDesignate = time;
            receiptFile = GlobalParams.recDesignateLocalPath;

            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream input = File.OpenRead(GlobalParams.recDesignateLocalPath))
            {
                rList = (List<Receipt>)formatter.Deserialize(input);
            }

            dgvReceipt.DataSource = new BindingList<Receipt>(rList);

            dgvReceipt.Columns["jh"].Visible = false;
            dgvReceipt.Columns["goodsCount"].Visible = false;
            dgvReceipt.Columns["moneyTotal"].Visible = false;
            dgvReceipt.Columns["yh"].Visible = false;
            dgvReceipt.Columns["zjj"].Visible = false;
            dgvReceipt.Columns["cash"].Visible = false;
            dgvReceipt.Columns["cardPay"].Visible = false;
            dgvReceipt.Columns["change"].Visible = false;
            dgvReceipt.Columns["mcard"].Visible = false;
            dgvReceipt.Columns["iniBalance"].Visible = false;
            dgvReceipt.Columns["endBalance"].Visible = false;

            dgvReceipt.ClearSelection();
        }

        #endregion

        /// <summary>
        /// 在datagridview里选择要补打的小票，在TextBox中预览内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceipt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //第一行列头要排除掉
            if (e.RowIndex > -1)
            {
                receipt = rList[e.RowIndex];
                Print prt = new Print();
                prt.rec = receipt;
                string reStr = prt.print(false);
                txtReceipt.Text = reStr;

                btnPrint.Enabled = true;
            }
        }

        #region 单点鼠标移动，模拟上滑和下滑

        Point _mousePoint;
        private void dgvReceipt_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mousePoint.X = e.X;
                _mousePoint.Y = e.Y;
            }
        }

        private void dgvReceipt_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Y - _mousePoint.Y > 20 && dgvReceipt.FirstDisplayedScrollingRowIndex > 0)
                    dgvReceipt.FirstDisplayedScrollingRowIndex--;

                if (e.Y - _mousePoint.Y < -20 && dgvReceipt.FirstDisplayedScrollingRowIndex < dgvReceipt.RowCount
                    && dgvReceipt.FirstDisplayedScrollingRowIndex > -1)
                    dgvReceipt.FirstDisplayedScrollingRowIndex++;
            }
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToString("yyyy年MM月dd日HH点mm分ss秒");
        }


        //字符串转换为整数
        public int stoi (String str){
    
        int b=Convert.ToInt32(str);
        return b;
    
    }

        public String getCbbMonthText() {
            DirectoryInfo m = new DirectoryInfo(GlobalParams.DataPath + "\\" + cbbYear.Text);
            Object[] dirsm = m.GetDirectories();
            cbbMonth.Text = dirsm[dirsm.Length - 1].ToString();
            return cbbMonth.Text;
        }
        public String getCbbYearText() { 
         DirectoryInfo di = new DirectoryInfo(GlobalParams.DataPath);
            Object[] dirs = di.GetDirectories();
            //排除suspendorder目录
            if (dirs.Length > 1)
                cbbYear.Text = dirs[dirs.Length - 2].ToString();
            return cbbYear.Text;
        }

        public void SeriOrder(List<Receipt> recipt)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream output = File.Create(DataPath + ".aaaaa"))
                {
                    formatter.Serialize(output, recipt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常信息" + ex.Message);
            }

        }

        public static string DataPath
        {
            get
            {
                //判断默认文件夹是否存在，不存在就创建
                //DateTime time = DateTime.Now;
                //int year = time.Year;
                //int month = time.Month;

                DirectoryInfo logDirectory = new DirectoryInfo(DateTime.Today.ToString("MM"));
                if (!logDirectory.Exists)
                    logDirectory.Create();

                return DateTime.Today.ToString("yyyyMMdd");
            }
        }
    }
}
