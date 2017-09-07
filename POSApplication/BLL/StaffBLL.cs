using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POSApplication.Model;
using POSApplication.DAL;

namespace POSApplication.BLL
{
    public class StaffBLL
    {
        /// <summary>
        /// 根据工号获取员工
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Staff GetStaffByID(string id)
        {
            return new StaffDAL().GetStaffByID(id);
        }

        /// <summary>
        /// 根据密码获取收银主管
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public Staff GetStaffByPwd(string pwd)
        {
            return new StaffDAL().GetStaffByPwd(pwd);
        }

        /// <summary>
        /// 清空员工表
        /// </summary>
        /// <returns></returns>
        public int DeleteStaff()
        {
            return new StaffDAL().DeleteStaff();
        }
    }
}
