using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using Shapeless.Core;

namespace Shapeless.MVC3
{
   public class Html 
    {
       public static MvcHtmlString HiddenFieldsFor(object obj)
       {
           FieldInfo[] fields =Clazz.fields(obj.GetType());
           string html = "";
           foreach (var field in fields)
           {

               string name = field.Name.TrimStart('_');
               object value = field.GetValue(obj);
               if (value == null) continue;
               html += string.Format("<input type='hidden' name='{0}' value='{1}' />"
                   , name
                   , value
                   );


           }
           return MvcHtmlString.Create(html);
       }
    }
}
