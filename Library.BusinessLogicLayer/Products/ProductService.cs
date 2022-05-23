using AutoMapper;
using Library.Common.Dtos;
using Library.DataAccessLayer;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Products
{
    public class ProductService : BasicService<long, Product, ProductDto, PageRequestDto>, IProductService
    {
        private readonly WebShopDbHelper _context;
        public ProductService(WebShopDbHelper context, IMapper mapper) : base(context.Product, mapper)
        {
            this._context = context;
        }

        public async Task<IList<ProductDto>> GetWithProductCategory(int? count)
        {
            var task = new Task<IList<ProductDto>>(() =>
            {
                var data = _context.Product.GetList(count);
                var result = data.Select(pro =>
                {
                    var proDto = mapper.Map<Product, ProductDto>(pro);
                    proDto.ProductCategory = _context.ProductCategory.Find(pro.Product_Category_Id);
                    //if (withImage)
                    //{
                    //    proDto.Image = _context.Image.Find(pro.Image_Id);
                    //}
                    return proDto;
                }).ToList();
                return result;
            });
            task.Start();
            await task;
            return task.Result;
        }
    }
}
