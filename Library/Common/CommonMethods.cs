using Library.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library.Common
{
    public static class CommonMethods
    {
        // auto insert 000
        public static string AutoInsertZero(int num)
        {
            if (num < 0)
            {
                num *= -1;
            }

            if (num.ToString().Length < 2)
            {
                return string.Format("00{0}", num);
            }

            if (num.ToString().Length < 3)
            {
                return string.Format("0{0}", num);
            }

            if (num.ToString().Length < 4)
            {
                return string.Format("{0}", num);
            }

            return string.Empty;
        }

        public static List<string> ListTimeInDays()
        {
            var times = new List<string>
            {
                "00:00",
                "00:30",
                "01:00",
                "01:30",
                "02:00",
                "02:30",
                "03:00",
                "03:30",
                "04:00",
                "04:30",
                "05:00",
                "05:30",
                "06:00",
                "06:30",
                "07:00",
                "07:30",
                "08:00",
                "08:30",
                "09:00",
                "09:30",
                "10:00",
                "10:30",
                "11:00",
                "11:30",
                "12:00",
                "12:30",
                "13:00",
                "13:30",
                "14:00",
                "14:30",
                "15:00",
                "15:30",
                "16:00",
                "16:30",
                "17:00",
                "17:30",
                "18:00",
                "18:30",
                "19:00",
                "19:30",
                "20:00",
                "20:30",
                "21:00",
                "21:30",
                "22:00",
                "22:30",
                "23:00",
                "23:30",
                "23:59",
            };
            return times;
        }

        public static List<T> RemoveDuplicates<T>(this List<T> list)
        {
            HashSet<T> hashset = new HashSet<T>();
            list.RemoveAll(x => !hashset.Add(x));
            return list;
        }

        public static string GetResourceTitle<T>(string key)
        {
            ResourceManager rm = new ResourceManager(typeof(T));
            string someString = rm.GetString(key);
            return someString;
        }

        public static IList<T> RemoveDuplicates<T>(this IList<T> list)
        {
            HashSet<T> hashset = new HashSet<T>();
            //list.RemoveAll(x => !hashset.Add(x));
            foreach (var item in list)
            {
                if (!hashset.Add(item))
                {
                    list.Remove(item);
                }
            }
            return list;
        }

        public static string SubStringBetween(string value, string a, string b)
        {
            int posA = value.IndexOf(a);
            int posB = value.LastIndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }

        public static List<Dictionary<string, object>> GetTableRows(DataTable data)
        {
            var lstRows = new List<Dictionary<string, object>>();
            foreach (DataRow row in data.Rows)
            {
                Dictionary<string, object> dictRow = new Dictionary<string, object>();
                foreach (DataColumn column in data.Columns)
                {
                    dictRow.Add(column.ColumnName, row[column]);
                }
                lstRows.Add(dictRow);
            }
            return lstRows;
        }

        public static DataTable ConvertDynamicToDataTable<T>(IEnumerable<T> data)
        {
            DataTable table = new DataTable();

            if (data != null && data.Count() > 0)
            {
                foreach (T item in data)
                {
                    if (item is IDictionary<string, object> dict)
                    {
                        foreach (var key in dict)
                        {
                            table.Columns.Add(key.Key, key.Value?.GetType() ?? typeof(object));

                        }
                        break;
                    }
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        table.Columns.Add(prop.Name, prop.PropertyType);
                    }
                    break;
                }

                foreach (T item in data)
                {

                    DataRow row;
                    if (item is IDictionary<string, object> dict)
                    {
                        row = table.NewRow();
                        foreach (var key in dict)

                        {
                            if (key.Key == "ExFty")
                            {

                                row[key.Key] = StringExtension.SubStringBeforeIndex(key.Value.ToString(), 10);
                            }
                            else
                            {
                                if (key.Value == null)
                                {
                                    row[key.Key] = 0;
                                }
                                else
                                {
                                    row[key.Key] = key.Value;
                                }
                            }

                        }

                        try
                        {
                            table.Rows.Add(row);
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                        continue;
                    }

                    row = table.NewRow();
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        row[prop.Name] = prop.GetValue(item);

                    }
                    table.Rows.Add(row);
                }
                //table.Columns["Container"].ColumnName = "CONT'S";
                //table.Columns["ExFty"].ColumnName = "Ex Fty";
                //table.Columns["DestinationCode"].ColumnName = "Ship To";
                //table.Columns["PODetailCode"].ColumnName = "Detail PO#";
                //table.Columns["POPartialCode"].ColumnName = "Partial PO#";
                //table.Columns["Code"].ColumnName = "SO";
                //table.Columns["ModelStyleName"].ColumnName = "Model Style";
                //table.Columns["ModelStyleCode"].ColumnName = "Style Code";
                //table.Columns["BaseColorName"].ColumnName = "Base Color";
                //table.Columns["SprayColorName"].ColumnName = "Spray Color";
                //table.Columns["PODetailOrderQuantity"].ColumnName = "Order QTY";
                //table.Columns["OrderQuantity"].ColumnName = "SO QTY";

                return table;
            }

            return null;
        }

        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            if (dt.Rows.Count > 0)
            {
                dt.Constraints.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    T item = GetItem<T>(row);
                    data.Add(item);
                }
            }
            return data;
        }

        public static DataTable ConvertListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        pro.SetValue(obj, dr[column.ColumnName] == DBNull.Value ? string.Empty :
      dr[column.ColumnName].ToString(), null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }

        public static T GetItemDataTable<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    //in case you have a enum/GUID datatype in your model
                    //We will check field's dataType, and convert the value in it.
                    if (pro.Name == column.ColumnName)
                    {
                        try
                        {
                            var convertedValue = GetValueByDataType(pro.PropertyType, dr[column.ColumnName]);
                            pro.SetValue(obj, convertedValue, null);
                        }
                        catch (Exception)
                        {
                            //ex handle code
                            throw;
                        }
                        //pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }

        public static object GetValueByDataType(Type propertyType, object o)
        {
            if (o.ToString() == "null")
            {
                return null;
            }
            if (propertyType == (typeof(Guid)) || propertyType == typeof(Guid?))
            {
                return Guid.Parse(o.ToString());
            }
            else if (propertyType == typeof(int) || propertyType.IsEnum)
            {
                return Convert.ToInt32(o);
            }
            else if (propertyType == typeof(decimal))
            {
                return Convert.ToDecimal(o);
            }
            else if (propertyType == typeof(long))
            {
                return Convert.ToInt64(o);
            }
            else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
            {
                return Convert.ToBoolean(o);
            }
            else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
            {
                return Convert.ToDateTime(o);
            }
            return o.ToString();
        }

        // using for export to excel in server side
        public static void WriteHtmlTable<T>(IEnumerable<T> data, TextWriter output, String[] labelList)
        {
            //Writes markup characters and text to an ASP.NET server control output stream. This class provides formatting capabilities that ASP.NET server controls use when rendering markup to clients.
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a form to contain the List
                    Table table = new Table();
                    TableRow row = new TableRow();
                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

                    // table header from IEnumerable
                    //foreach (PropertyDescriptor prop in props) {
                    //    TableHeaderCell hcell = new TableHeaderCell();
                    //    hcell.Text = prop.Name;
                    //    hcell.BackColor = System.Drawing.Color.Yellow;
                    //    row.Cells.Add(hcell);
                    //}

                    foreach (String label in labelList)
                    {
                        using (TableHeaderCell hcell = new TableHeaderCell())
                        {
                            hcell.Text = label;
                            //hcell.BackColor = System.Drawing.Color.Yellow;
                            hcell.Font.Bold = true;
                            row.Cells.Add(hcell);
                        }
                        row.BorderStyle = BorderStyle.Solid;
                    }
                    table.Rows.Add(row);

                    //  add each of the data item to the table
                    foreach (T item in data)
                    {
                        row = new TableRow();
                        foreach (PropertyDescriptor prop in props)
                        {
                            TableCell cell = new TableCell
                            {
                                Text = prop.Converter.ConvertToString(prop.GetValue(item))
                            };
                            //cell.BorderStyle = BorderStyle.Solid;
                            row.Cells.Add(cell);
                            row.BorderStyle = BorderStyle.Solid;
                        }
                        table.Rows.Add(row);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response
                    output.Write(sw.ToString());
                }
            }
        }

        public static Dictionary<string, string> PagingAndOrderBy(Paging pageing, string orderByStr)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            int pageIndex = pageing.Page;
            int pageSize = pageing.Rows;
            int start_r = pageing.Page > 1 ? ((pageIndex - 1) * pageSize) : pageing.Page;
            int end_r = (pageIndex * pageSize);
            string order_by = pageing.Sidx != null ? $"{pageing.Sidx} {pageing.Sord}" : orderByStr;
            list.Add("index", pageIndex.ToString());
            list.Add("size", pageSize.ToString());
            list.Add("start", start_r.ToString());
            list.Add("end", end_r.ToString());
            list.Add("orderBy", order_by);
            return list;
        }

        public static ResponseModel<T> ExceptionReturn<T>(ResponseModel<T> returnData)
        {
            returnData.Data = default;
            returnData.IsSuccess = false;
            returnData.ResponseMessage = Resource.ERROR_SystemError;
            returnData.HttpResponseCode = 500;
            return returnData;
        }
    }
}
