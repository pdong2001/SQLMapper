using Library.BusinessLogicLayer.ProductDetails;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Invoices
{
    public class InvoiceDetailDto : InvoiceDetail
    {
        public ProductDetailDto Product_Detail { get; set; }
    }
}
