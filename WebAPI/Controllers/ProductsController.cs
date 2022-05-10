using Library.BusinessLogicLayer.Products;
using Library.Common.Dtos;
using Library.DataAccessLayer;
using Library.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _product;

        public ProductsController(IProductRepository product)
        {
            this._product = product;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int limit)
        {
            try
            {
                var result =await _product.GetList(limit);
                return Ok(new ServiceResponse<IList<Product>> { Code = 200, Status = true, Data = result, Message="Successful"});
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<Product>> { Code = 500, Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetPage([FromQuery] PagedAndSortedLookUpDto request)
        {
            try
            {
                var result =await _product.Pagination(request);
                return Ok(new ServiceResponse<PagedAndSortedResultDto<Product>> { Code = 200, Status = true, Data = result, Message = "Successful" });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<Product>> { Code = 500, Status = false, Message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                var result = await _product.Find(id);
                if (result == null)
                {
                    return Ok(new ServiceResponse<Product> { Code = 404, Status = false, Message = "Not found" });
                }
                return Ok(new ServiceResponse<Product> { Code = 200, Status = true, Data = result, Message = "Successful" });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<Product>> { Code = 500, Status = false, Message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product input)
        {
            try
            {
                var result = await _product.Create(input);
                if (result == null)
                {
                    return Ok(new ServiceResponse<Product> { Code = 400, Status = false, Message = "Failed" });
                }
                return Ok(new ServiceResponse<Product> { Code = 200, Status = true, Data = result, Message = "Successful" });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<Product>> { Code = 500, Status = false, Message = e.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody] Product input)
        {
            try
            {
                var result = await _product.Update(id,input);
                return Ok(new ServiceResponse<bool> { Code = 200, Status = result, Data = result });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<Product>> { Code = 500, Status = false, Message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var result = await _product.Delete(id);
                return Ok(new ServiceResponse<bool> { Code = 200, Status = result, Data = result });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<Product>> { Code = 500, Status = false, Message = e.Message });
            }
        }
    }
}
