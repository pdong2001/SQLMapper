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
        public Category Category { get; set; }
        public Provider Provider { get; set; }
        public IEnumerable<ProductDetail> Details { get; set; }
        public new Blob Default_Image { get; set; }
        public IEnumerable<ImageAssign> Images { get; set; }
        public IEnumerable<ProductOption> Options { get; set; }
    }
}
