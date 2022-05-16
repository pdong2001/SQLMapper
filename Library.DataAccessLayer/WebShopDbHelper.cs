using Library.Common;
using Library.DataAccessLayer.EntitySets;
using Library.DataModels;
using System;

namespace Library.DataAccessLayer
{
    public class WebShopDbHelper : DatabaseHelper
    {
        public WebShopDbHelper(string connectionString) : base(connectionString)
        {
        }

        public EntitySet<ProductCategory> ProductCategory { get; set; }
        public EntitySet<Product> Product { get; set; }
    }
}
