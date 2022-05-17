using Library.BusinessLogicLayer.ProductCategories;
using Library.Common.Dtos;
using Library.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/admin/product-category")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _category;

        public ProductCategoryController(IProductCategoryService category)
        {
            this._category = category;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? limit)
        {
            try
            {
                var result = await _category.GetListAsync(limit);
                return Ok(new ServiceResponse<IList<ProductCategoryDto>> { Code = 200, Status = true, Data = result, Message = "Successful" });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<ProductCategory>> { Code = 500, Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("includes")]
        public async Task<IActionResult> GetWithProduct([FromQuery] int? limit)
        {
            try
            {
                var result = await _category.GetWithProduct(limit);
                return Ok(new ServiceResponse<IList<ProductCategoryDto>> { Code = 200, Status = true, Data = result, Message = "Successful" });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<ProductCategory>> { Code = 500, Status = false, Message = e.Message });
            }
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetPage([FromQuery] PageRequestDto request)
        {
            try
            {
                var result = await _category.PaginationAsync(request);
                return Ok(new ServiceResponse<PagedAndSortedResultDto<ProductCategoryDto>> { Code = 200, Status = true, Data = result, Message = "Successful" });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<ProductCategory>> { Code = 500, Status = false, Message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            try
            {
                var result = await _category.FindAsync(id);
                if (result == null)
                {
                    return Ok(new ServiceResponse<ProductCategory> { Code = 404, Status = false, Message = "Not found" });
                }
                return Ok(new ServiceResponse<ProductCategory> { Code = 200, Status = true, Data = result, Message = "Successful" });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<ProductCategory>> { Code = 500, Status = false, Message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCategory input)
        {
            try
            {
                var result = await _category.CreateAsync(input);
                if (result == null)
                {
                    return Ok(new ServiceResponse<ProductCategory> { Code = 400, Status = false, Message = "Failed" });
                }
                return Ok(new ServiceResponse<ProductCategory> { Code = 200, Status = true, Data = result, Message = "Successful" });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<ProductCategory>> { Code = 500, Status = false, Message = e.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] ProductCategory input)
        {
            try
            {
                var result = await _category.UpdateAsync(id, input);
                return Ok(new ServiceResponse<bool> { Code = 200, Status = result, Data = result });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<ProductCategory>> { Code = 500, Status = false, Message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            try
            {
                var result = await _category.DeleteAsync(id);
                return Ok(new ServiceResponse<bool> { Code = 200, Status = result, Data = result });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<ProductCategory>> { Code = 500, Status = false, Message = e.Message });
            }
        }
    }
}
