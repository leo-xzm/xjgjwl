using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;

namespace POSApplication.Common
{
    public class ORM<ItemType> where ItemType : class,new()
    {
        private PropertyInfo[] _props;

        public PropertyInfo[] Props
        {
            get
            {
                if (_props == null)
                {
                    _props = GetPropertyInfoArray(typeof(ItemType));
                }
                return _props;
            }
        }

        /// <summary>
        /// 反射出属性数组
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected PropertyInfo[] GetPropertyInfoArray(Type type)
        {
            PropertyInfo[] props = type.GetProperties();
            List<PropertyInfo> list = new List<PropertyInfo>();
            object[] objAttributes = null;
            foreach (PropertyInfo pi in props)
            {
                //pi.
                objAttributes = pi.GetCustomAttributes(typeof(ColumnAttributes), true);
                if (objAttributes.Length > 0)
                {
                    if (objAttributes[0] is ColumnAttributes)
                    {
                        list.Add(pi);
                    }
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 将DataTable转换成List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<ItemType> SwichByDT(DataTable dt)
        {
            List<ItemType> list = new List<ItemType>();
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            ItemType item;
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                item = SwichByDR(dr);
                list.Add(item);
            }
            return list;
        }

        #region 把行转换成对象
        /// <summary>
        /// 把1行转换成对象
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public ItemType SwichByDR(DataRow dr)
        {
            if (dr == null) return null;
            ItemType item = new ItemType();
            string propName;
            object propvalue;
            foreach (System.Reflection.PropertyInfo prop in Props)
            {
                propName = prop.Name;
                if (!dr.Table.Columns.Contains(propName)) continue;//如果在DT上找不到此列，则忽略此列
                propvalue = dr[propName];
                SetValue(item, prop, propvalue);
            }
            return item;
        }
        /// <summary>
        /// 为对象的属性设置值
        /// </summary>
        /// <param name="item">实体</param>
        /// <param name="prop">属性元数据</param>
        /// <param name="propvalue">属性值</param>
        protected void SetValue(ItemType item, System.Reflection.PropertyInfo prop, object propvalue)
        {
            if (propvalue is DBNull || propvalue == null) return;
            {
                try
                {
                    switch (prop.PropertyType.ToString())
                    {
                        case "System.String":
                            prop.SetValue(item, propvalue.ToString().Trim(), null);
                            break;
                        case "System.Boolean":
                            prop.SetValue(item, ((Convert.ToInt32(propvalue) == 0) ? false : true), null);
                            break;
                        case "System.Int32":
                            prop.SetValue(item, Convert.ToInt32(propvalue), null);
                            break;
                        case "System.Int64":
                            prop.SetValue(item, Convert.ToInt64(propvalue), null);
                            break;
                        case "System.Int16":
                            prop.SetValue(item, Convert.ToInt16(propvalue), null);
                            break;
                        case "System.Decimal":
                            prop.SetValue(item, decimal.Round(Convert.ToDecimal(propvalue), 2), null);
                            break;
                        case "System.DateTime":
                            if (propvalue != null)
                            {
                                prop.SetValue(item, Convert.ToDateTime(propvalue), null);
                            }
                            else
                            {
                                prop.SetValue(item, null, null);
                            }

                            break;
                        default:
                            prop.SetValue(item, propvalue, null);
                            break;
                    }
                }
                catch
                {
                    prop.SetValue(item, propvalue.ToString(), null);
                }
            }
        }

        #endregion
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttributes : System.Attribute
    {
        private string _columnName = string.Empty;
        private bool _isIdentity = false;

        public string ColumnName
        {
            get { return _columnName; }
        }

        public bool IsIdentity
        {
            get { return _isIdentity; }
            set { _isIdentity = value; }
        }

        public ColumnAttributes(string columnName)
        {
            _columnName = columnName;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttributes : System.Attribute
    {
        private string _name = string.Empty;

        public string Name
        {
            get { return _name; }
        }

        public TableAttributes(string name)
        {
            _name = name;
        }
    }
}
