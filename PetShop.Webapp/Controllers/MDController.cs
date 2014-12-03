using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using PetShop.Webapp.Models;

namespace PetShop.Webapp.Controllers
{
    public abstract class MDController<M> : Shapeless.MVC3.Controllers.LinqMDController<M> where M:class,new()
    {
        protected override DataContext dataContext
        {
            get
            {
                return new PetShopTableDataContext();
            }
        }


        protected override M newM
        {
            get { return new M(); }
        }

        protected override bool isUndefined(string val)
        {
            return string.IsNullOrWhiteSpace(val) || val == "0";
        }

        protected override void _create(ref M m, ref Shapeless.MVC3.Models.Result re)
        {
           table.InsertOnSubmit(m);
        }

        protected override void _update(ref M m, ref Shapeless.MVC3.Models.Result re)
        {
            
        }

        protected override void _delete(ref M m, ref Shapeless.MVC3.Models.Result re)
        {
          table.DeleteOnSubmit(m);
        }

        
    }
}