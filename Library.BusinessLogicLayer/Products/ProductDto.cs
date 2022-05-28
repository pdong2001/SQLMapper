using Library.BusinessLogicLayer.ProductDetails;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Products
{
    public class ProductDto : Product
    {
        public Blob Image { get; set; }
        public IList<ProductDetailDto> Details { get; set; }
        public Category Category { get; internal set; }
    }
}
