using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shapeless.MVC3.Controllers
{
    public abstract class MonthSqlLSController :SqlLSController
    {
        protected abstract int year { get; }
        protected abstract int month { get; }

        protected abstract string dayColumn(DateTime dt);


        protected override string columnNames
        {
            get
            {
                string colnames = base.columnNames, monthdaycols="";
                DateTime dt = new DateTime(year, month, 1);
                
                while (dt.Month <= month && dt.Year <= year)
                {
                    monthdaycols += "," + dayColumn(dt);
                    dt = dt.AddDays(1);
                }
                return colnames.Trim(',')+","+monthdaycols.Trim(',');

            }
        }
    }
}
