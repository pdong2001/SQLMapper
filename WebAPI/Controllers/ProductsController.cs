using Library.BusinessLogicLayer.Products;
using Library.Common.Dtos;
using Library.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ProductsController : BaseAPIController<long, SanPham, SanPhamDto, TimKiemSanPhamDto>
    {

        public ProductsController(ISanPhamService products) : base(products)
        {
        }
    }
}
