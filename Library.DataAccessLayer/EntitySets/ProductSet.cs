using Library.Common;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccessLayer.EntitySets
{
    public class ProductSet : EntitySet<Product>
    {
        public ProductSet(SqlConnection connection) : base(connection)
        {
        }

        protected override string TableName => "Products";

        public override bool Delete(object id)
        {
            /*
             * Code pre-delete here.
             */
            return base.Delete(id);
        }
    }
}
