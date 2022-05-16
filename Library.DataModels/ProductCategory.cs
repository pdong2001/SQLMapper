using Library.Common.Attributes;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    [TableName("ProductCategories")]
    public class ProductCategory : AuditedEntity<long>
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        public int Status { get; set; }
    }
}
