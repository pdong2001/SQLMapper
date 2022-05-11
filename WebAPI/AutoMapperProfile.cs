using AutoMapper;
using Library.BusinessLogicLayer.Categories;
using Library.DataModels;

namespace WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
