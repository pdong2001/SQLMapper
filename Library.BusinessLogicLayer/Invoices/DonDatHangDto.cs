using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Invoices
{
    public class DonDatHangDto : DonDatHang
    {
        public IList<ChiTietDonDatHang> details { get; set; }
        public string Status_Name { get => DonDatHang.TenTrangThai(this.Status); }
    }
}
