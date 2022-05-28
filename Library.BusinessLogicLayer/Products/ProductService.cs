using AutoMapper;
using Library.BusinessLogicLayer.Blobs;
using Library.BusinessLogicLayer.ProductDetails;
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
        private readonly WebShopDbHelper _dbHelper;
        private readonly IProductDetailService _productDetails;
        private readonly IBlobService _blobService;
        public ProductService(WebShopDbHelper dbHelper, IMapper mapper, IProductDetailService productDetails, IBlobService blobService) : base(dbHelper.Products, mapper)
        {
            _dbHelper = dbHelper;
            _productDetails = productDetails;
            _blobService = blobService;
        }

        public override PagedAndSortedResultDto<ProductDto> Pagination(PageRequestDto request)
        {
            var data = base.Pagination(request);
            data.Items.ToList().ForEach(item =>
            {
                item.Details = _productDetails.GetList(null, new DbQueryParameterGroup(new DbQueryParameter(nameof(ProductDetail.Product_Id), item.Id, CompareOperator.Equal)));
                if (item.Default_Image.HasValue)
                    item.Image = _blobService.Find(item.Default_Image.Value);
                if (item.Category_Id.HasValue)
                    item.Category = _dbHelper.Categories.Find(item.Category_Id.Value);
            });
            return data;
        }
    }
}
