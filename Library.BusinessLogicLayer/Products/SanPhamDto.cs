using Library.BusinessLogicLayer.ProductDetails;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Products
{
    public class SanPhamDto : SanPham
    {
        public int Min_Price { get; set; }
        public Blob Image { get; set; }
        public IList<ChiTietSanPhamDto> Details { get; set; }
        public LoaiSanPham Category { get; internal set; }
        public ChiTietSanPhamDto Default_Detail { get; set; }
    }
}
