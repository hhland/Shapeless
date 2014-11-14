using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shapeless.MVC3.Models
{
     public class Result
    {

          public static int CODE_SUCCESS = 0;
            public static int CODE_ALERT = 1;
            public static int CODE_ERROR = 2;
            public static int CODE_EXCEPTION = 3;

            public static string ICON_INFO = "info";
            public static string ICON_ERROR = "error";
            public static string ICON_QUESTION = "question";
            public static string ICON_WARNING = "warning";

            public static string ACTION_SHOW = "show";
            public static string ACTION_CONFIRM = "confirm";
            public static string ACTION_ALERT = "alert";
            public static string ACTION_PROMPT = "prompt";


            public Result()
            {
                this.code = CODE_SUCCESS;
                this.msg = "";
                this.title = "";
                this.action = ACTION_SHOW;
                this.icon = ICON_INFO;
                this.attrs = new Dictionary<string, object>();
                this.rowNum = 0;
                this.traceStack = new Dictionary<string, string>();
            }

            public int code { get; set; }
            public string msg { get; set; }
            public string title { get; set; }
            public string action { get; set; }
            public string icon { get; set; }
            public int rowNum { get; set; }


            public Dictionary<string, object> attrs { get; set; }

            public Dictionary<string, string> traceStack { get; set; }
    }
}
