using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetShop.Webapp.Models;

namespace PetShop.Webapp.Controllers
{
    public abstract class LSController : Shapeless.MVC3.Controllers.SqlLSController
    {






        protected override System.Data.SqlClient.SqlConnection connection
        {
            get { return 
                new SqlConnection(
                    new PetShopTableDataContext().Connection.ConnectionString
                    );
                 }
        }

        protected override string createSelectWithOrderbySql(int start, int limit)
        {
            throw new NotImplementedException();
        }

        
    }
}
