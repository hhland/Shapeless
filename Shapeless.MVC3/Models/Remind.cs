using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shapeless.MVC3.Models
{
    public struct Remind
    {
        public string title
        {
            get;
            set;
        }

        public string text
        {
            get;
            set;
        }

        public int total
        {
            get;
            set;
        }

        public int code
        {
            get;
            set;
        }

        public string target
        {
            get;
            set;
        }

        public string src
        {
            get;
            set;
        }

        public string style
        {
            get;
            set;
        }
    }
}
