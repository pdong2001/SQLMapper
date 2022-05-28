using Library.BusinessLogicLayer.ProductDetails;
using Library.Common.Dtos;
using Library.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/admin/product-details")]
    [ApiController]
    public class ProductDetailDetailsController : BaseAPIController<long, ProductDetail, ProductDetailDto, PageRequestDto>
    {
        public ProductDetailDetailsController(IProductDetailService productDetails) : base(productDetails)
        {
        }
    }
}
