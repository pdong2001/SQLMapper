using Library.Common.Attributes;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    public class ProductDetailOptionValue : Entity<long>
    {
        public string Value { get; set; }
        
        [ForeignKey(typeof(ProductOption))]
        public long Option_Id { get; set; }

        [ForeignKey(typeof(ProductDetail))]
        public long Product_Detail_Id { get; set; }
    }
}
