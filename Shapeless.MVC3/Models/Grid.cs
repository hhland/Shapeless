using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shapeless.MVC3.Models
{
    public struct Grid
    {
        public int total { get; set; }

        public IList<IDictionary<string, object>> rows { get; set; }
    }
}
