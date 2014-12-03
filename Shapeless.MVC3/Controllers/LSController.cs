using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Shapeless.MVC3.Models;

namespace Shapeless.MVC3.Controllers
{
    public abstract class LSController : Controller
    {

        protected abstract string title { get; }

        protected virtual int limit
        {
            get { return Int32.MaxValue; }
        }

        protected virtual int start
        {
            get { return 0; }
        }

        protected abstract int _total();

        public ActionResult total()
        {

            return Content(_total().ToString());
        }

        protected abstract IList<IDictionary<string, object>> _rows();

        public ActionResult rows()
        {
            return Json(_rows(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult grid()
        {
            Grid grid = new Grid
            {
                rows = _rows()
            };
            int rcount = grid.rows.Count;
            grid.total = rcount > limit ? _total() : rcount;
            return Json(grid, JsonRequestBehavior.AllowGet);
        }

        protected virtual string _help()
        {
            return "";
        }

    

        public ActionResult help()
        {
            return Content(_help());
        }

    }
}
