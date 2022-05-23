using AutoMapper;
using Library.Common.Dtos;
using Library.Common.Interfaces;
using Library.DataAccessLayer;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Invoices
{
    public class InvoiceService : BasicService<long, Invoice, InvoiceDto, PageRequestDto>, IInvoiceService
    {
        private readonly WebShopDbHelper _shopDbHelper;
        public InvoiceService(WebShopDbHelper webShopDbHelper, IMapper mapper)
            : base(webShopDbHelper.Invoices, mapper)
        {
            this._shopDbHelper = webShopDbHelper;
        }

        public IList<InvoiceDto> GetWithDetail(int? Count)
        {
            var data = _shopDbHelper.Invoices.GetList(Count);

            var invoiceDtos = data.Select(i =>
            {
                var obj = this.mapper.Map<Invoice, InvoiceDto>(i);
                obj.Details = _shopDbHelper.InvoiceDetails
                    .GetList(null, new DbQueryParameter
                    {
                        Name = nameof(InvoiceDetail.Invoice_Id),
                        Value = obj.Id,
                        CompareOperator = CompareOperator.Equal
                    });
                return obj;
            }).ToList();
            return invoiceDtos;
        }

    }
}
