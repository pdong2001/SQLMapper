using Library.Common.Attributes;
using Library.Common.Dtos;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Library.DataModels
{
    [TableName("Products")]
    public class Product : AuditedEntity<long>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey(typeof(Category))]
        public long ? Category_Id { get; set; }

        [ForeignKey(typeof(Provider))]
        public long? Provider_Id { get; set; }

        public int Quantity { get; set; }

        public int Option_Count { get; set; }

        [ForeignKey(typeof(Blob))]
        public long? Default_Image { get; set; }
    }
}
