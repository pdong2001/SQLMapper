﻿using Library.BusinessLogicLayer.Products;
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
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _product;

        public ProductsController(IProductService product)
        {
            this._product = product;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int limit)
        {
            try
            {
                var result =await _product.GetListAsync(limit);
                return Ok(new ServiceResponse<IList<ProductDto>> { Code = 200, Status = true, Data = result, Message="Successful"});
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<Product>> { Code = 500, Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetPage([FromQuery] PageRequestDto request)
        {
            try
            {
                var result = await _product.PaginationAsync(request);
                return Ok(new ServiceResponse<PagedAndSortedResultDto<ProductDto>> { Code = 200, Status = true, Data = result, Message = "Successful" });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<Product>> { Code = 500, Status = false, Message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            try
            {
                var result = await _product.FindAsync(id);
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
                var result = await _product.CreateAsync(input);
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
        public async Task<IActionResult> Update([FromRoute] long id,[FromBody] Product input)
        {
            try
            {
                var result = await _product.UpdateAsync(id,input);
                return Ok(new ServiceResponse<bool> { Code = 200, Status = result, Data = result });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<Product>> { Code = 500, Status = false, Message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            try
            {
                var result = await _product.DeleteAsync(id);
                return Ok(new ServiceResponse<bool> { Code = 200, Status = result, Data = result });
            }
            catch (Exception e)
            {
                return Ok(new ServiceResponse<IList<Product>> { Code = 500, Status = false, Message = e.Message });
            }
        }
    }
}
