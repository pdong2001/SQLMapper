using Library.Common.Attributes;
using Library.Common.Interfaces;

namespace Library.DataModels
{
    public class Receipt : AuditedEntity<long>
    {
        [ForeignKey(typeof(ProductDetail))]
        public long? Product_Detail_Id { get;set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        [ForeignKey(typeof(Provider))]
        public long? Provider_Id { get; set; }
    }
}