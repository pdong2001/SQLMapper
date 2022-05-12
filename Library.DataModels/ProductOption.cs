using Library.Common.Attributes;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    public class ProductOption : Entity<long>
    {
        public string Name { get; set; }

        [ForeignKey(typeof(Product))]
        public long Product_Id { get; set; }
    }
}
