using Library.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Products
{
    public class TimKiemSanPhamDto : PageRequestDto
    {
        public bool With_Detail { get; set; } = false;
        public long? category { get; set; }
        public bool visible_only { get; set; } = false;

    }
}
