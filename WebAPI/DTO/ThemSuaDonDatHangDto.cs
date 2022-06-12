using Library.DataModels;
using System.Collections.Generic;

namespace WebAPI.DTO
{
    public class ThemSuaDonDatHangDto : DonDatHang
    {
        public IList<ChiTietDonDatHang> Details { get; set; }
    }
}
