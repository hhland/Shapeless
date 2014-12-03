using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Shapeless.MVC3.Models;

namespace Shapeless.MVC3.Controllers
{
    public abstract class DatatableLSController :LSController
    {

        protected abstract DataTable createDataTable();

        protected override IList<IDictionary<string, object>> _rows()
        {
            return Grid.createBy(createDataTable()).rows;
        }

        protected override int _total()
        {
            return Grid.createBy(createDataTable()).total;
        }
    }
}
