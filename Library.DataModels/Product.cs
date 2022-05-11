using Library.Common.Interfaces;
using System;

namespace Library.DataModels
{
    public class Product : AuditedEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long ? Category_Id { get; set; }
        public long? Provider_Id { get; set; }
        public int Quantity { get; set; }
        public int Option_Count { get; set; }
    }
}
