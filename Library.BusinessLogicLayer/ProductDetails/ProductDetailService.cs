using AutoMapper;
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

namespace Library.BusinessLogicLayer.ProductDetails
{
    public class ProductDetailService : BasicService<long, ProductDetail, ProductDetailDto, ProductDetailLoopUpDto>, IProductDetailService
    {
        private readonly WebShopDbHelper _dbHelper;
        public ProductDetailService(IMapper mapper, WebShopDbHelper webShopDbHelper) : base(webShopDbHelper.ProductDetails, mapper)
        {
            this._dbHelper = webShopDbHelper;
        }

        public override PagedAndSortedResultDto<ProductDetailDto> Pagination(ProductDetailLoopUpDto request)
        {
            var data = base.Pagination(request);
            data.Items.Select(prod =>
            {
                if (request.With_Product)
                {
                    prod.Product = mapper.Map<Product, ProductDto>(_dbHelper.Products.Find(prod.Product_Id));
                }
                //if (prod.Default_Image.HasValue)
                return prod;
            });
            return data;
        }

        public override ProductDetailDto Find(long id)
        {
            var data = base.Find(id);
            //if (data.Default_Image.HasValue)
                //data.Image = _dbHelper.Blobs.Find(data.Default_Image.Value);
            data.Product = _dbHelper.Products.Find(data.Product_Id);
            return data;
        }
    }
}
