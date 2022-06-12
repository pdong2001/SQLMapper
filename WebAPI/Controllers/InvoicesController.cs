using Library.BusinessLogicLayer.Invoices;
using Library.Common.Dtos;
using Library.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPI.DTO;

namespace WebAPI.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class InvoicesController : BaseAPIController<long, DonDatHang, DonDatHangDto, TimKiemDonDatHangDto>
    {

        public InvoicesController(IDonDatHangService donDatHang) : base(donDatHang)
        {
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateWithDetail([FromBody] ThemSuaDonDatHangDto request)
        {
            var service = this._service as IDonDatHangService;
            var response = new ServiceResponse<long>();
            var result = service.CreateWithDetail(request, request.Details);
            if (result > 0)
            {
                response.SetSuccess(result);
            }
            else
            {
                response.SetFailed();
            }
            return Ok(response);
        }
    }
}
