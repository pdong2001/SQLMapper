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
        public InvoiceService(WebShopDbHelper shopDbHelper, IMapper mapper) : base(shopDbHelper.Invoices, mapper)
        {  
            this._shopDbHelper = shopDbHelper;
        }
    }
}
