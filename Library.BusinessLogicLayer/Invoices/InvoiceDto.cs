using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Invoices
{
    public class InvoiceDto : Invoice
    {
        public IList<InvoiceDetail> Details { get; set; }
    }
}
