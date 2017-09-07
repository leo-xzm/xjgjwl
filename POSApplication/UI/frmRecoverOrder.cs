using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POSApplication.Model;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using POSApplication.Common;

namespace POSApplication.UI
{
   

    public partial class frmRecoverOrder : Form
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
        /// 要恢复的挂单文件名
        /// </summary>
        private string orderToRecover { get; set; }

        /// <summary>
        /// 挂单数据显示
        /// </summary>
        private List<OrderItem> order { get; set; }

       public frmRecoverOrder(){
            InitializeComponent();
            staff = new Staff();
            staff.Staff_name = "ddd";
        }
        public frmRecoverOrder(Staff staff, frmMain main)
        {
            InitializeComponent(); 
            
            this.staff = staff;
            this.main = main;
        }

        private void frmRecoverOrder_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lbJH.Text = "机号：" + ConfigHelper.GetAppConfig("JH");
            lbStaff.Text = "员工：" + staff.Staff_name;

            cbbSuspendList_DataBind();

            dgvGoods.RowsDefaultCellStyle.ForeColor = SystemColors.WindowText;
            dgvGoods.RowsDefaultCellStyle.Font = new Font("宋体", 20, FontStyle.Regular);
            dgvGoods.RowHeadersDefaultCellStyle.Font = new Font("宋体", 9, FontStyle.Regular);
        }

        #region 挂单列表相关
        /// <summary>
        /// 显示挂单列表
        /// </summary>
        private void cbbSuspendList_DataBind()
        {
            cbbSuspendList.Items.Clear();
            cbbSuspendList.Text = "";

            //读取挂单数据目录，显示存在的文件
            DirectoryInfo di = new DirectoryInfo(GlobalParams.suspendOrderPath);
            List<FileInfo> fList = di.GetFiles().ToList();
            if (fList.Count > 0)
            {
                fList.Reverse();
                foreach (FileInfo fi in fList)
                {
                    string day = fi.Name.Substring(4, 4);
                    string time = fi.Name.Substring(8, 2) + ":" + fi.Name.Substring(10, 2) + ":" + fi.Name.Substring(12, 2);

                    ComboBoxItem newItem = new ComboBoxItem();
                    newItem.Value = fi.Name;
                    newItem.Text = day + " " + time;

                    cbbSuspendList.Items.Add(newItem);
                }

                cbbSuspendList.SelectedItem = cbbSuspendList.Items[0];
                btnDeleteAll.Enabled = true;
            }
            else
            {
                btnDeleteAll.Enabled = false;
                dgvIni();
            }
        }

        /// <summary>
        /// 选择挂单文件，显示购物单具体数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbSuspendList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cbbSuspendList.SelectedItem;

            order = new List<OrderItem>();

            BinaryFormatter formatter = new BinaryFormatter();
            //反序列化
            using (Stream input = File.OpenRead(GlobalParams.suspendOrderPath + item.Value))
            {
                if (input.Length > 0)
                    order = (List<OrderItem>)formatter.Deserialize(input);
            }

            //成功读取到临时购物单
            if (order.Count > 0)
            {
                orderToRecover = item.Value;

                dgvGoods.DataSource = new BindingList<OrderItem>(order.Where(o => o.isValid == true).ToList());

                dgvGoods.Columns["xpxh"].Visible = false;
                dgvGoods.Columns["txm"].Visible = false;
                dgvGoods.Columns["number"].Visible = false;
                dgvGoods.Columns["Mcard"].Visible = false;
                dgvGoods.Columns["Mname"].Visible = false;
                dgvGoods.Columns["Balance"].Visible = false;
                dgvGoods.Columns["YJ"].Visible = false;
                dgvGoods.Columns["sell_price"].Visible = false;
                dgvGoods.Columns["m_sell_price"].Visible = false;
                dgvGoods.Columns["jf_rate"].Visible = false;
                dgvGoods.Columns["isValid"].Visible = false;
                dgvGoods.Columns["yhmx"].Visible = false;
                dgvGoods.Columns["cxbh"].Visible = false;
                dgvGoods.Columns["cxid"].Visible = false;

                dgvGoods.RowsDefaultCellStyle.Font = new Font("宋体", 20, FontStyle.Regular);

                dgvGoods.ClearSelection();

                btnDelete.Enabled = true;
                btnRecover.Enabled = true;
            }
        }

        #endregion

        private void btnRecover_Click(object sender, EventArgs e)
        {
            if (File.Exists(GlobalParams.orderPath))
            {
                DialogResult dr = MessageBox.Show("存在尚未结账的交易！要恢复此挂单吗？", "存在尚未结账的交易", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    File.Delete(GlobalParams.orderPath);
                    File.Move(GlobalParams.suspendOrderPath + orderToRecover, GlobalParams.orderPath);
                }
            }
            else
            {
                File.Move(GlobalParams.suspendOrderPath + orderToRecover, GlobalParams.orderPath);
                MessageBox.Show("此挂单已恢复！");
            }

            //重新绑定
            cbbSuspendList_DataBind();
        }
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("要删除此挂单吗？", "删除挂单", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                File.Delete(GlobalParams.suspendOrderPath + orderToRecover);

                //重新绑定
                cbbSuspendList_DataBind();
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("要清空列表吗？", "清空列表", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Directory.Delete(GlobalParams.suspendOrderPath, true);

                //重新绑定
                cbbSuspendList_DataBind();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            main.Show();
            this.Dispose();
        }

        #region 单点鼠标移动，模拟上滑和下滑

        Point _mousePoint;
        private void dgvGoods_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mousePoint.X = e.X;
                _mousePoint.Y = e.Y;
            }
        }

        private void dgvGoods_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Y - _mousePoint.Y > 20 && dgvGoods.FirstDisplayedScrollingRowIndex > 0)
                    dgvGoods.FirstDisplayedScrollingRowIndex--;

                if (e.Y - _mousePoint.Y < -20 && dgvGoods.FirstDisplayedScrollingRowIndex < dgvGoods.RowCount
                    && dgvGoods.FirstDisplayedScrollingRowIndex > -1)
                    dgvGoods.FirstDisplayedScrollingRowIndex++;
            }
        }
        #endregion


        private void dgvIni()
        {
            //清空dgv
            order = new List<OrderItem>();
            dgvGoods.DataSource = new BindingList<OrderItem>(order);
            dgvGoods.Columns["xpxh"].Visible = false;
            dgvGoods.Columns["txm"].Visible = false;
            dgvGoods.Columns["number"].Visible = false;
            dgvGoods.Columns["Mcard"].Visible = false;
            dgvGoods.Columns["Mname"].Visible = false;
            dgvGoods.Columns["Balance"].Visible = false;
            dgvGoods.Columns["YJ"].Visible = false;
            dgvGoods.Columns["sell_price"].Visible = false;
            dgvGoods.Columns["m_sell_price"].Visible = false;
            dgvGoods.Columns["jf_rate"].Visible = false;
            dgvGoods.Columns["isValid"].Visible = false;
            dgvGoods.Columns["yhmx"].Visible = false;
            dgvGoods.Columns["cxbh"].Visible = false;
            dgvGoods.Columns["cxid"].Visible = false;

            btnRecover.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToString("yyyy年MM月dd日HH点mm分ss秒");
        }
    }
    public class ComboBoxItem
    {
        private string _text = null;
        private string _value = null;
        public string Text { get { return this._text; } set { this._text = value; } }
        public string Value { get { return this._value; } set { this._value = value; } }
        public override string ToString()
        {
            return this._text;
        }
    }
}
