using Library.BusinessLogicLayer.ProductDetails;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Receipts
{
    public class ReceiptDto : Receipt
    {
        public ProductDetailDto Product { get; set; }
    }
}
