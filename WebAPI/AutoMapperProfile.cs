using AutoMapper;
using Library.BusinessLogicLayer.Categories;
using Library.BusinessLogicLayer.Invoices;
using Library.BusinessLogicLayer.Products;
using Library.Common.Dtos;
using Library.DataModels;

namespace WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Invoice, InvoiceDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<PagedAndSortedResultDto<Product>, PagedAndSortedResultDto<ProductDto>>();
        }
    }
}
