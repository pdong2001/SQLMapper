using AutoMapper;
using Library.BusinessLogicLayer.ProductDetails;
using Library.BusinessLogicLayer.Products;
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
    public class InvoiceService : BasicService<long, Invoice, InvoiceDto, InvoiceLookUpDto>, IInvoiceService
    {
        private readonly WebShopDbHelper _shopDbHelper;
        private readonly IProductDetailService _productDetailService;
        public InvoiceService(WebShopDbHelper webShopDbHelper, IMapper mapper, IProductDetailService productDetailService)
            : base(webShopDbHelper.Invoices, mapper)
        {
            this._shopDbHelper = webShopDbHelper;
            _productDetailService = productDetailService;
        }

        public override PagedAndSortedResultDto<InvoiceDto> Pagination(InvoiceLookUpDto request)
        {
            var data = base.Pagination(request);
            data.Items.Select(i =>
            {
                i.Details = _shopDbHelper.InvoiceDetails.GetList(null, new DbQueryParameterGroup(LogicOperator.AND, new DbQueryParameter
                {
                    Value = i.Id,
                    Name = nameof(InvoiceDetail.Invoice_Id),
                    CompareOperator = CompareOperator.Equal
                })).Select(detail =>
                {
                    var dto = mapper.Map<InvoiceDetail, InvoiceDetailDto>(detail);
                    if (dto.Product_Detail_Id.HasValue)
                        dto.Product_Detail = _productDetailService.Find(dto.Product_Detail_Id.Value);
                    dto.Product_Detail.Product = mapper.Map<Product, ProductDto>(_shopDbHelper.Products.Find(dto.Product_Detail.Product_Id));
                    return dto;
                }).ToList();
                return i;
            });
            return data;
        }
    }
}
