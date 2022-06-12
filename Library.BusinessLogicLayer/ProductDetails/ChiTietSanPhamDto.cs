using Library.BusinessLogicLayer.Products;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.ProductDetails
{
    public class ChiTietSanPhamDto : ChiTietDoUong
    {
        public DoUongDto Product { get; set; }
        public Blob Image { get; set; }

    }
}
