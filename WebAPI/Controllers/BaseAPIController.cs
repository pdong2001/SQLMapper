using Library.BusinessLogicLayer;
using Library.Common.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    public abstract class BaseAPIController<TKey, TEntity, TDto, TPageRequest> : ControllerBase 
        where TKey : struct where TPageRequest : PageRequestDto
    {
        protected IBasicService<TKey, TEntity, TDto, TPageRequest> _service;

        protected BaseAPIController(IBasicService<TKey, TEntity, TDto, TPageRequest> service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("list")]
        public virtual IActionResult GetList([FromQuery] int? count)
        {
            var response = new ServiceResponse<IList<TDto>>();
            response.SetSuccess(_service.GetList(count));
            return Ok(response);
        }

        [HttpGet]
        public virtual IActionResult Paginate([FromQuery] TPageRequest request)
        {
            var response = new ServiceResponse<IList<TDto>>();
            var data = _service.Pagination(request);
            response.SetSuccess(data.Items);
            response.SetPaginationData(data);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public virtual IActionResult GetById([FromRoute] TKey id)
        {
            var response = new ServiceResponse<TDto>();
            var data = _service.Find(id);
            if (data != null)
                response.SetSuccess(data);
            else
            {
                response.SetFailed();
            }
            return Ok(response);
        }

        [HttpPost]
        public virtual IActionResult Create([FromBody] TEntity product)
        {
            var response = new ServiceResponse<TDto>();
            response.SetSuccess(_service.Create(product));
            return Ok(response);
        }

        [HttpPut, HttpPatch]
        [Route("{id}")]
        public virtual IActionResult Edit([FromRoute] TKey id, [FromBody] TEntity product)
        {
            var response = new ServiceResponse<bool>();
            response.SetSuccess(_service.Update(id, product));
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public virtual IActionResult Delete([FromRoute] TKey id)
        {
            var response = new ServiceResponse<bool>();
            response.SetSuccess(_service.Delete(id));
            return Ok(response);
        }
    }
}
