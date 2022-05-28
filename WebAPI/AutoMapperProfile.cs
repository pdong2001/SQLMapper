using AutoMapper;
using Library.BusinessLogicLayer.Blobs;
using Library.BusinessLogicLayer.Categories;
using Library.BusinessLogicLayer.ProductDetails;
using Library.BusinessLogicLayer.Products;
using Library.BusinessLogicLayer.Providers;
using Library.BusinessLogicLayer.Receipts;
using Library.Common.Dtos;
using Library.DataModels;

namespace WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Product,ProductDto>();
            CreateMap<ProductDetail, ProductDetailDto>();
            CreateMap<Provider, ProviderDto>();
            CreateMap<Blob, BlobDto>();
            CreateMap<Receipt, ReceiptDto>();
        }
    }
}
