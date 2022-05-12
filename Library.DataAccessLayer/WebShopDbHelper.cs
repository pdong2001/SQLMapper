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
        public ProductSet Products { get; set; }
        public CategorySet Categories { get; set; }
        public EntitySet<Provider> Providers { get; set; }
        public EntitySet<ProductDetail> ProductsDetails { get; set; }
        public EntitySet<ProductOption> ProductOptions { get; set; }
        public EntitySet<ProductDetailOptionValue> ProductDetailOptionValues { get; set; }
        public EntitySet<Blob> Blobs { get; set; }
        public EntitySet<ImageAssign> ImageAssigns { get; set; }
        public EntitySet<Customer> Customers { get; set; }
    }
}
