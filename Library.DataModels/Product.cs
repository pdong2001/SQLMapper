using Library.Common.Attributes;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    [TableName("Products")]
    public class Product : AuditedEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; } = 0;
        public string Note { get; set; }
        public bool Visible { get; set; }
        public int Option_Count { get; set; } = 0;

        [ForeignKey(typeof(Blob))]
        public long? Default_Image { get; set; }

        [ForeignKey(typeof(Category))]
        public long? Category_Id { get; set; }

        [ForeignKey(typeof(Provider))]
        public long? Provider_Id { get; set; }

        public string Code { get; set; }
    }
}
