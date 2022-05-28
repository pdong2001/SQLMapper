using Library.BusinessLogicLayer.Providers;
using Library.Common.Dtos;
using Library.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ProvidersController : BaseAPIController<long, Provider, ProviderDto, PageRequestDto>
    {

        public ProvidersController(IProviderService providers) : base(providers)
        {
        }
    }
}
