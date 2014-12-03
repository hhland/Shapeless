using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Shapeless.Core;
using Shapeless.MVC3.Models;

namespace Shapeless.MVC3.Controllers.Ext
{
   public abstract class TreeSqlLSController :SqlLSController
    {

       public const string ATTR_CHILDREN = "children",
           FIELD_LEVEL="level",FIELD_ID="id",FIELD_PID="pid"
           ;

       protected string root
       {
           get
           {
               return Request.Params["root"];
           }
       }

       protected int level
       {
           get
           {
               string level = Request.Params[FIELD_LEVEL];
               if (string.IsNullOrWhiteSpace(level))
               {
                   return -1;
               }
               else
               {
                   return Int32.Parse(level);
               }
           }
       }

       protected virtual bool checkable {
           get { return false; }
       }

       protected override string createConditionSql()
       {
           string cond= base.createConditionSql();
           if (!Stringz.isNullOrWhiteSpace(root, cond))
           {
               cond += " or " +FIELD_ID +" = '"+root+"'";
           }
           return cond;
       }


       protected virtual JArray _nodes(IList<IDictionary<string, object>> list,string pid,JArray jarr)
       {
           foreach (IDictionary<string, object> dict in list)
           {
               if (!dict.ContainsKey(FIELD_PID)) break;

               if (dict[FIELD_PID].ToString() != pid) continue;

               //string jStr = JsonConvert.SerializeObject(dict);
               JObject jObj = JObject.FromObject(dict); // JsonConvert.DeserializeObject<JObject>(jStr);
               JArray jChildren = _nodes(list, dict[FIELD_ID].ToString(), new JArray());
               jObj.Add(ATTR_CHILDREN, jChildren);
               if (jChildren.Count == 0)
               {
                   
                    jObj.Add("leaf", true);
                   


               }

               if (checkable)
               {
                   jObj.Add("checked", false);
               }
               if (dict.ContainsKey("icon"))
               {
                   jObj["icon"] = dict["icon"].ToString();
                  
               }
               if (dict.ContainsKey("expanded"))
               {
                   string value = dict["expanded"].ToString();
                   if (!string.IsNullOrWhiteSpace(value))
                   {
                       jObj["expanded"] = value == "1";
                   }
               }
               jarr.Add(jObj);

           }
           return jarr;
       }

       /// <summary>
       /// tree数据源 JSON数组
       /// </summary>
       /// <param name="pk"></param>
       /// <returns></returns>
       public  ActionResult nodes(string pk)
       {
           
           
           
           JArray jarr = new JArray();
           
           string sql = createSelectWithOrderbySql();

           IList<IDictionary<string, object>> list = null;
          
           
               try
               {
                   
                   list = Sqlz.list(connection,sql);
                   jarr = _nodes(list, root, jarr);
                  
               }
               catch (Exception e)
               {
                   Result jsRO = new Result();
                   jsRO.code = Result.CODE_EXCEPTION;
                   jsRO.msg = e.Message;
                   return Json(jsRO, JsonRequestBehavior.AllowGet);
               }
           
           return JavaScript(jarr.ToString());
       }


       protected virtual JArray _levels(IList<IDictionary<string, object>> list, int le, JArray jarr)
       {
           
           foreach (IDictionary<string, object> dict in list.Where(dict=>(int)dict[FIELD_LEVEL]==le).ToArray())
           {
               if (!dict.ContainsKey(FIELD_PID)) break;

              

               //string jStr = JsonConvert.SerializeObject(dict);
               JObject jObj = JObject.FromObject(dict); // JsonConvert.DeserializeObject<JObject>(jStr);
               JArray jChildren = _levels(list, le+1, new JArray());
               jObj.Add(ATTR_CHILDREN, jChildren);
               if (checkable)
               {
                   jObj.Add("checked", false);
               }
               if (dict.ContainsKey("icon"))
               {
                   jObj["icon"] = dict["icon"].ToString();

               }
               if (dict.ContainsKey("expanded"))
               {
                   string value = dict["expanded"].ToString();
                   if (!string.IsNullOrWhiteSpace(value))
                   {
                       jObj["expanded"] = value == "1";
                   }
               }
               jarr.Add(jObj);

           }
           return jarr;
       }

       public virtual ActionResult levels(string pk)
       {
           JArray jarr = new JArray();

           string sql = createSelectWithOrderbySql();
         
           IList<IDictionary<string, object>> list = null;
           try
           {
               list = Sqlz.list(connection,sql);
               jarr = _levels(list, level, jarr);
           }
           catch (Exception e)
           {
               Result jsRO = new Result();
               jsRO.code = Result.CODE_EXCEPTION;
               jsRO.msg = e.Message;
               return Json(jsRO, JsonRequestBehavior.AllowGet);
           }
           return Content(jarr.ToString());
       }
    }
}
