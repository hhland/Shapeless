using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Shapeless.Core
{
    public class Sqlz
    {

        public static IList<IDictionary<string, object>> list(SqlConnection connection, string sql)
        {
            IList<IDictionary<string, object>> list = new List<IDictionary<string, object>>();
            SqlCommand cmd = new SqlCommand(sql, connection);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                DataTable dataTable = reader.GetSchemaTable();
                //DataTable dataTable= reader.GetSchemaTable();
                while (reader.Read())
                {
                    IDictionary<string, object> dict = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        object val = reader.GetValue(i);
                        if (val == null) continue;
                        dict.Add(dataTable.Rows[i][0].ToString(), reader.GetValue(i));
                    }
                    list.Add(dict);
                }
            }
            return list;
        }

    }
}
