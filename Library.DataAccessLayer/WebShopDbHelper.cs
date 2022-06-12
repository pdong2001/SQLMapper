using Library.Common;
using Library.Common.Interfaces;
using Library.DataModels;
using System;

namespace Library.DataAccessLayer
{
    public class WebShopDbHelper : DatabaseHelper
    {
        public WebShopDbHelper(string connectionString) : base(connectionString)
        {
        }

        public EntitySet<LoaiSanPham> DSLoaiSanPham { get; set; }
        public EntitySet<SanPham> DSSanPham { get; set; }
        public EntitySet<ChiTietSanPham> DSCTSanPham { get; set; }
        public EntitySet<ChiTietDonDatHang> DSCTDonDatHang { get; set; }
        public EntitySet<DonDatHang> DSDonDatHang { get; set; }
        public EntitySet<Blob> Blobs { get; set; }
    }
}
