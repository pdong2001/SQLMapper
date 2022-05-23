using Library.BusinessLogicLayer.Categories;
using Library.BusinessLogicLayer.ProductDetails;
using Library.DataModels;
using System.Collections.Generic;

namespace Library.BusinessLogicLayer.Products
{
    public class ProductDto : Product
    {
        public IEnumerable<ProductDetailDto> Details { get; set; }
        public CategoryDto Category { get; set; }
    }
}