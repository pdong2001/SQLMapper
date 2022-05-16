using AutoMapper;
using Library.BusinessLogicLayer.ProductCategories;
using Library.BusinessLogicLayer.Products;
using Library.Common.Dtos;
using Library.DataModels;

namespace WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductCategory, ProductCategoryDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<PagedAndSortedResultDto<Product>, PagedAndSortedResultDto<ProductDto>>();
        }
    }
}
