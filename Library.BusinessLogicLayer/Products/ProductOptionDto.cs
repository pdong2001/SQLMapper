using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Products
{
    public class ProductDetailOptionValueDto : ProductDetailOptionValue
    {
        public ProductOption Option { get; set; }
        public string Name { get; set; }
    }
}
