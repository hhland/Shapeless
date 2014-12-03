using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetShop.Webapp.Controllers
{
    public class LS_CategoryController : LSController
    {


        protected override string viewName
        {
            get { return "Category"; }
        }

        protected override string title
        {
            get { return "Category"; }
        }
    }
}
