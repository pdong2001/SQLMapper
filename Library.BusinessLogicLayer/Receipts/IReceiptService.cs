using Library.Common.Dtos;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Receipts
{
    public interface IReceiptService : IBasicService<long, Receipt, ReceiptDto, PageRequestDto>
    {
    }
}
