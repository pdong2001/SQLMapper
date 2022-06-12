using Library.BusinessLogicLayer.Categories;
using Library.Common.Dtos;
using Library.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class CategoriesController : BaseAPIController<long, LoaiDoUong, LoaiDoUongDto, PageRequestDto>
    {
        public CategoriesController(ILoaiDoUongService categories) : base(categories)
        {
        }
    }
}
