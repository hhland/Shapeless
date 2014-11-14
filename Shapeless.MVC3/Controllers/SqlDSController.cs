using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Shapeless.Core;
using Shapeless.MVC3.Models;

namespace Shapeless.MVC3.Controllers
{
    public abstract class SqlDSController :DSController
    {

        protected abstract string viewName { get;}

        protected abstract SqlConnection connection { get; }

       


       


        

        protected virtual string columnNames
        {
            get { return "*"; }
        }


        protected virtual string baseCondition
        {
            get  { return ""; }
        }

        protected virtual string postCondition
        {

            get
            {
                string condition = "";
                foreach (string name in Request.Params.Keys)
                {
                    string value = Request.Params[name];
                    if (string.IsNullOrWhiteSpace(value)) continue;
                    if (name.StartsWith("qc_like_"))
                    {
                        condition += string.Format(" and {0} like '%{1}%' ", name.Substring("q_like_".Length), value);
                    }
                    else if (name.StartsWith("qc_concatlike_"))
                    {
                        string[] names = name.Substring("qc_concatlike_".Length).Split(',');
                        condition += string.Format(" and ({0}) like '%{1}%' ",
                            string.Join("+','+", names),
                            value);
                    }
                    else if (name.StartsWith("qc_eq_"))
                    {
                        condition += string.Format(" and {0} = '{1}' ", name.Substring("q_eq_".Length), value);
                    }
                    else if (name.StartsWith("qc_in_"))
                    {
                        string[] vals = value.Split(',');

                        condition += string.Format(" and {0} in ('{1}') ", name.Substring("q_in_".Length), string.Join("','", vals));
                    }
                    else if (name.StartsWith("qn_in_"))
                    {
                        condition += string.Format(" and {0} in ({1}) ", name.Substring("qn_in_".Length), value);
                    }
                    else if (name.StartsWith("qc_nin_"))
                    {
                        string[] vals = value.Split(',');
                        condition += string.Format(" and {0} not in ({1}) ", name.Substring("q_nin_".Length), string.Join("','", vals));
                    }
                    else if (name.StartsWith("qn_nin_"))
                    {
                        condition += string.Format(" and {0} not in ('{1}') ", name.Substring("qn_nin_".Length), value);
                    }
                    else if (name.StartsWith("qn_eq_"))
                    {
                        condition += string.Format(" and  {0} = {1} ", name.Substring("qn_eq_".Length), value);
                    }
                    else if (name.StartsWith("qn_gt_"))
                    {
                        condition += string.Format(" and {0} > {1} ", name.Substring("qn_gt_".Length), value);
                    }
                    else if (name.StartsWith("qn_lt_"))
                    {
                        condition += string.Format(" and {0} < {1} ", name.Substring("qn_lt_".Length), value);
                    }
                    else if (name.StartsWith("qn_gte_"))
                    {
                        condition += string.Format(" and {0} >= {1} ", name.Substring("qn_gte_".Length), value);
                    }
                    else if (name.StartsWith("qn_lte_"))
                    {
                        condition += string.Format(" and {0} <= {1} ", name.Substring("qn_lte_".Length), value);
                    }
                    else if (name.StartsWith("qn_from_"))
                    {
                        string subname = name.Substring("qn_from_".Length);
                        string toname = "qn_to_" + subname;
                        string toValue = Request.Params[toname];
                        if (string.IsNullOrWhiteSpace(toValue))
                        {
                            condition += string.Format(" and {0} = {1} ", subname, value, Request.Params[toname]);
                        }
                        else
                        {
                            condition += string.Format(" and {0} between {1} and  {2} ", subname, value, Request.Params[toname]);
                        }
                    }
                    else if (name.StartsWith("qd_from_"))//value格式为"yyyy-MM-dd"
                    {
                        string subname = name.Substring("qd_from_".Length);
                        string toname = "qd_to_" + subname;
                        string toValue = Request.Params[toname];
                        if (string.IsNullOrWhiteSpace(toValue)) continue;

                        string tovalue = Request.Params[toname];
                        int datetimelength = "yyyy-MM-dd".Length;
                        if (value.Length == datetimelength) value += " 00:00:00";
                        if (tovalue.Length == datetimelength) tovalue += " 23:59:59";
                        condition += string.Format(" and {0} between '{1}' and  '{2}' ", subname, value, tovalue);
                    }
                    else if (name.StartsWith("qd_gte_"))
                    {
                        condition += string.Format(" and {0} >='{1}' ", name.Substring("qd_gte_".Length), value);
                    }
                   
                }
                condition = condition.Trim().Trim("and".ToCharArray());
                return condition;

            }

        }

        protected virtual string baseOrderby
        {
            get { return ""; }
        }

        protected virtual string postOrderby
        {
            get
            {
                string sort = Request.Params["sort"];
                string dir = Request.Params["dir"];
                if (string.IsNullOrWhiteSpace(sort) || string.IsNullOrWhiteSpace(dir))
                {
                    return "";
                }
                return sort + " " + dir;
            }
        }


        /// <summary>
        /// 生成数据查询sql
        /// </summary>
        /// <returns></returns>
        protected virtual string createConditionSql()
        {

            string condition = "";
               
            ;
            if (!String.IsNullOrWhiteSpace(baseCondition) && !String.IsNullOrWhiteSpace(postCondition))
            {
                condition = string.Format(" where ({0}) and ( {1} ) ", baseCondition, postCondition);
            }
            else if (!String.IsNullOrWhiteSpace(baseCondition))
            {
                condition = string.Format(" where ({0}) ", baseCondition);
            }
            else if (!String.IsNullOrWhiteSpace(postCondition))
            {
                condition = string.Format(" where ({0}) ", postCondition);
            }

            return condition;
        }

        protected  string createSelectSql()
        {

            return "select " + columnNames + " from " + viewName + " " + createConditionSql();
        }

        protected  string createSelectCountSql()
        {

            return "select count(*) from " + viewName + " " + createConditionSql();
        }

        protected  string createSelectWithOrderbySql()
        {

            return "select count(*) from " + viewName + " " + createConditionSql();
        }

        protected abstract string createSelectWithOrderbySql(int start, int limit);
        
        

        protected override int _total()
        {
            string sqlCount = createConditionSql();
            int re = 0;
            using (DataContext dc=new DataContext(connection))
            {
                re = dc.ExecuteQuery<int>(sqlCount).First<int>();
            }
            return re;
        }

        

        protected override IList<IDictionary<string,object>> _rows()
        {
            string sql = createSelectWithOrderbySql(start,limit);
            IList<IDictionary<string,object>> rows = Sqlz.list(connection, sql);
            return rows;
        }

       


        

      

        

    }
}
