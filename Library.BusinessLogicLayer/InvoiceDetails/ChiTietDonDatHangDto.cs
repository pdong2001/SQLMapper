using Library.BusinessLogicLayer.Invoices;
using Library.BusinessLogicLayer.ProductDetails;
using Library.DataModels;

namespace Library.BusinessLogicLayer.InvoiceDetails
{
    public class ChiTietDonDatHangDto : ChiTietDonDatHang
    {
        public DonDatHangDto invoice { get; set; }
        public ChiTietSanPhamDto Product_Detail { get; set; }
    }
}