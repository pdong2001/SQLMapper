using Library.Common.Attributes;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    public class ProductDetail : AuditedEntity<long>
    {
        public long Product_Id { get; set; }
        public string Unit { get; set; }
        public string Note { get; set; }
        public bool Visible { get; set; }
        [ForeignKey(typeof(Blob))]
        public long? Default_Image { get; set; }
        public int In_Price { get; set; }
        public int Out_Price { get; set; }
        public int Remaining_Quantity { get; set; }
        public int Total_Quantity { get; set; }
    }
}
