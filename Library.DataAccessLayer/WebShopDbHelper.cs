using Library.Common;
using Library.Common.Interfaces;
using Library.DataModels;
using System;

namespace Library.DataAccessLayer
{
    public class WebShopDbHelper : DatabaseHelper
    {
        public WebShopDbHelper(string connectionString) : base(connectionString)
        {
        }

        public EntitySet<Category> Categories { get; set; }
        public EntitySet<Product> Products { get; set; }
        public EntitySet<Provider> Providers { get; set; }
        public EntitySet<ProductDetail> ProductDetails { get; set; }
        public EntitySet<Receipt> Receipts { get; set; }
        public EntitySet<Blob> Blobs { get; set; }
    }
}
