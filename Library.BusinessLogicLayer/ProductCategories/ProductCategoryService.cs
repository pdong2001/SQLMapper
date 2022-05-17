using AutoMapper;
using Library.Common.Dtos;
using Library.DataAccessLayer;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.ProductCategories
{
    public class ProductCategoryService : BasicService<long, ProductCategory, ProductCategoryDto, PageRequestDto>, IProductCategoryService
    {
        private readonly WebShopDbHelper _context;
        public ProductCategoryService(WebShopDbHelper context, IMapper mapper) : base(context.ProductCategory, mapper)
        {
            this._context = context;
        }

        public async Task<IList<ProductCategoryDto>> GetWithProduct(int? count)
        {
            var task = new Task<IList<ProductCategoryDto>>(() =>
            {
                var data = _context.ProductCategory.GetList(count);
                var result = data.Select(cate =>
                {
                    var cateDto = mapper.Map<ProductCategory, ProductCategoryDto>(cate);
                    cateDto.Products = _context.Product.GetList(null, new DbQueryParameter { Name = nameof(Product.Product_Category_Id), CompareOperator = CompareOperator.Equal, Value = cateDto.Id });
                    return cateDto;
                }).ToList();
                return result;
            });
            task.Start();
            await task;
            return task.Result;
        }
    }
}
