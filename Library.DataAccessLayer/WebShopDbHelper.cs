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

        public EntitySet<LoaiDoUong> DSLoaiDoUong { get; set; }
        public EntitySet<DoUong> DSDoUong { get; set; }
        public EntitySet<ChiTietDoUong> DSCTSanPham { get; set; }
        public EntitySet<ChiTietDonDatHang> DSCTDonDatHang { get; set; }
        public EntitySet<DonDatHang> DSDonDatHang { get; set; }
        public EntitySet<Blob> Blobs { get; set; }
    }
}
