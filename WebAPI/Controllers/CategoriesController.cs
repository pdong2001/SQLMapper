using Library.BusinessLogicLayer.Categories;
using Library.Common.Dtos;
using Library.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categories;

        public CategoriesController(ICategoryRepository categories)
        {
            this._categories = categories;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? limit)
        {
            try
            {
                var result = await _categories.GetList(limit);
                return Ok(new ServiceResponse<IList<Category>> { Code = 200, Status = true, Data = result, Message = "Successful" });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<Category>> { Code = 500, Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("includes")]
        public async Task<IActionResult> GetWithProduct([FromQuery] int? limit)
        {
            try
            {
                var result = await _categories.GetWithProduct(limit);
                return Ok(new ServiceResponse<IList<CategoryDto>> { Code = 200, Status = true, Data = result, Message = "Successful" });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<Category>> { Code = 500, Status = false, Message = e.Message });
            }
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetPage([FromQuery] PagedAndSortedLookUpDto request)
        {
            try
            {
                var result = await _categories.Pagination(request);
                return Ok(new ServiceResponse<PagedAndSortedResultDto<Category>> { Code = 200, Status = true, Data = result, Message = "Successful" });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<Category>> { Code = 500, Status = false, Message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            try
            {
                var result = await _categories.Find(id);
                if (result == null)
                {
                    return Ok(new ServiceResponse<Category> { Code = 404, Status = false, Message = "Not found" });
                }
                return Ok(new ServiceResponse<Category> { Code = 200, Status = true, Data = result, Message = "Successful" });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<Category>> { Code = 500, Status = false, Message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category input)
        {
            try
            {
                var result = await _categories.Create(input);
                if (result == null)
                {
                    return Ok(new ServiceResponse<Category> { Code = 400, Status = false, Message = "Failed" });
                }
                return Ok(new ServiceResponse<Category> { Code = 200, Status = true, Data = result, Message = "Successful" });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<Category>> { Code = 500, Status = false, Message = e.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] Category input)
        {
            try
            {
                var result = await _categories.Update(id, input);
                return Ok(new ServiceResponse<bool> { Code = 200, Status = result, Data = result });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<Category>> { Code = 500, Status = false, Message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            try
            {
                var result = await _categories.Delete(id);
                return Ok(new ServiceResponse<bool> { Code = 200, Status = result, Data = result });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<Category>> { Code = 500, Status = false, Message = e.Message });
            }
        }
    }
}
