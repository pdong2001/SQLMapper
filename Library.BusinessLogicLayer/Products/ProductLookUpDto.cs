using Library.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Products
{
    public class ProductLookUpDto : PageRequestDto
    {
        public bool With_Detail { get; set; }
        public bool Consumeanle_Only { get; set; }
        public long Category { get; set; }
    }
}
