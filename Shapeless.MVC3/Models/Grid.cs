using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Shapeless.MVC3.Models
{
    public struct Grid
    {
        public int total { get; set; }

        public IList<IDictionary<string, object>> rows { get; set; }


        public static Grid createBy(DataTable dt)
        {
            return createBy(dt, 0, Int32.MaxValue);
        }

        public static Grid createBy(DataTable dt, int start, int limit)
        {
            Grid griData = new Grid
            {
                total = dt.Rows.Count
                ,
                rows = new List<IDictionary<string, object>>()
            };

            foreach (DataRow row in dt.Rows)
            {
                IDictionary<string, object> dict = new Dictionary<string, object>();
                foreach (DataColumn column in dt.Columns)
                {
                    string colname = column.ColumnName;
                    object val = row[colname];
                    if (val == null) continue;
                    dict[colname] = val;
                }
                griData.rows.Add(dict);
            }
            return griData;
        }
    }
}
