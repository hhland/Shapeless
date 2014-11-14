using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace Shapeless.MVC3.Controllers
{
    public abstract class LinqMDController<M> : MDController<M> where M:class 
    {
        protected abstract DataContext dataContext { get; }

        protected virtual Table<M> table
        {
            get { return dataContext.GetTable<M>(); }
        }

        protected override void _create(ref M m, ref Models.Result re)
        {
            table.InsertOnSubmit(m);
        }

        protected override void _update(ref M m, ref Models.Result re)
        {
            
        }

        protected override void _delete(ref M m, ref Models.Result re)
        {
            table.DeleteOnSubmit(m);
        }

       
    }
}
