using Library.Common.Dtos;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Invoices
{
    public interface IInvoiceService : IBasicService<long, Invoice, InvoiceDto, PageRequestDto>
    {
        /// <summary>
        /// Lấy kèm theo chi tiết
        /// </summary>
        /// <returns>Danh sách hóa đơn kèm theo</returns>
        IList<InvoiceDto> GetWithDetail(int? Count);
    }
}
