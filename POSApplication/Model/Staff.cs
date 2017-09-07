using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POSApplication.Common;

namespace POSApplication.Model
{
    /// <summary>
    /// staff_ygk: 员工实体类
    /// </summary>
    [Serializable]
    [TableAttributes("Staff")]
    public class Staff
    {
        public Staff()
        { }
        #region Model
        private string _staff_no;
        private string _staff_name;
        private string _rank_no;
        private string _password;

        /// <summary>
        /// 工号
        /// </summary>
        [ColumnAttributes("Staff_no")]
        public string Staff_no
        {
            set { _staff_no = value; }
            get { return _staff_no; }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        [ColumnAttributes("Staff_name")]
        public string Staff_name
        {
            set { _staff_name = value; }
            get { return _staff_name; }
        }

        /// <summary>
        /// 权限类别
        /// </summary>
        [ColumnAttributes("Rank_no")]
        public string Rank_no
        {
            set { _rank_no = value; }
            get { return _rank_no; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        [ColumnAttributes("password")]
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        #endregion Model
    }
}
