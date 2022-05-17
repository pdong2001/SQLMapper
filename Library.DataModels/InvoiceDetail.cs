using Library.Common.Attributes;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    [TableName("InvoiceDetails")]
    public class InvoiceDetail : AuditedEntity<long>
    {
        [ForeignKey(typeof(Invoice))]
        public long Invoice_Id { get; set; }

        public int Quantity { get; set; }

        [ForeignKey(typeof(ProductDetail))]
        public long Product_Detail_Id { get; set; }

        public long Price { get; set; }
    }
}
