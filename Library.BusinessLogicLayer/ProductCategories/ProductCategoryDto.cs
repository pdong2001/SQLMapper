using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.ProductCategories
{
    public class ProductCategoryDto : ProductCategory
    {
        public IEnumerable<Product> Products { get; set; }
    }
}
