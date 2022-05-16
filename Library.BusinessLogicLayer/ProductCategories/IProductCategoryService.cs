using Library.Common.Dtos;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.ProductCategories
{
    public interface IProductCategoryService : IBasicService<long, ProductCategory, ProductCategoryDto, PageRequestDto>
    {
        Task<IList<ProductCategoryDto>> GetWithProduct(int? count);
    }
}
