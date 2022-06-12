using Library.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.ProductDetails
{
    public class TimKiemCTSanPhamDto : PageRequestDto
    {
        public bool With_Product { get; set; } = false;
        public bool consumable_only { get; set; } = false;
        public long? product_id { get; set; }
    }
}
