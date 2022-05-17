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
    public class ProductCategorySet : EntitySet<ProductCategory>
    {
        public ProductCategorySet(SqlConnection connection) : base(connection)
        {
        }


    }
}
