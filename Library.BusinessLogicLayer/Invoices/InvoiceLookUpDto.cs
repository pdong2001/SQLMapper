using Library.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Invoices
{
    public class InvoiceLookUpDto : PageRequestDto
    {
        public bool With_Detail { get; set; } = false;
        public bool With_Product { get; set; } = false;
    }
}
