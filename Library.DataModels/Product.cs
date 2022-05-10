using Library.Common.Interfaces;
using System;

namespace Library.DataModels
{
    public class Product : AuditedEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ? CategoryId { get; set; }
        public int Quantity { get; set; }
    }
}
