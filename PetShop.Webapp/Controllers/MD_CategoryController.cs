using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetShop.Webapp.Models;

namespace PetShop.Webapp.Controllers
{
    public class MD_CategoryController : MDController<Category>
    {
        protected override string title
        {
            get { return "MD_CategoryController"; }
        }

        protected override Category _read(string pk)
        {
            return table.Single(m => m.CatId == pk);
        }
    }
}