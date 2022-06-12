using Library.Common.Attributes;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    [TableName("DSChiTietDonDatHang")]
    public class ChiTietDonDatHang : Entity<long>
    {
        [ForeignKey(typeof(DonDatHang))]
        public long Invoice_Id { get; set; }
        [ForeignKey(typeof(ChiTietDoUong))]
        public long? Product_Detail_id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
