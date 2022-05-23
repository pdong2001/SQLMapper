using AutoMapper;
using Library.BusinessLogicLayer.Categories;
using Library.BusinessLogicLayer.ProductDetails;
using Library.Common.Dtos;
using Library.Common.Interfaces;
using Library.DataAccessLayer;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Products
{
    public class ProductService : BasicService<long, Product, ProductDto, ProductLookUpDto>, IProductService
    {
        private readonly WebShopDbHelper _dbHelper;
        public ProductService(IMapper mapper, WebShopDbHelper webShopDbHelper) : base(webShopDbHelper.Products, mapper)
        {
            this._dbHelper = webShopDbHelper;
        }
        public override PagedAndSortedResultDto<ProductDto> Pagination(ProductLookUpDto request)
        {
            var data = base.Pagination(request);
            data.Items.Select(prod =>
            {
                if (request.With_Category && prod.Category_Id.HasValue)
                {
                    prod.Category = mapper.Map<Category, CategoryDto>(_dbHelper.Categories.Find(prod.Category_Id.Value));
                }
                return prod;
            });
            return data;
        }
    }
}
