using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;

namespace Shapeless.MVC3.Controllers
{
    public abstract class LinqMEController<M> :MEController<M> where M:class,new()
    {
        protected abstract DataContext dataContext { get; }
    
        protected virtual Table<M> table {
            get { return dataContext.GetTable<M>(); } 
        }

        protected override void eachRow(int rowindex, IXLRow row, M m)
        {
            if(m==null)return;
            table.InsertOnSubmit(m);
        }

        protected override void onImported()
        {
            dataContext.SubmitChanges();
        }
    }
}
