using Library.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Invoices
{
    public class TimKiemDonDatHangDto : PageRequestDto
    {
        public bool With_Detail { get; set; } = false;
    }
}
