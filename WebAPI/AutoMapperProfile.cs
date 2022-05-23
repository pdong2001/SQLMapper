using AutoMapper;
using Library.BusinessLogicLayer.Categories;
using Library.BusinessLogicLayer.ProductDetails;
using Library.BusinessLogicLayer.Invoices;
using Library.Common.Dtos;
using Library.DataModels;
using Library.BusinessLogicLayer.Providers;

namespace WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<ProductDetail, ProductDetailDto>();
            CreateMap<Invoice, InvoiceDto>();
            CreateMap<Provider, ProviderDto>();
        }
    }
}
