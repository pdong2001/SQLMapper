using Library.BusinessLogicLayer.InvoiceDetails;
using Library.Common.Dtos;
using Library.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/admin/invoice-details")]
    [ApiController]
    public class InvoiceDetailsController : BaseAPIController<long, ChiTietDonDatHang, ChiTietDonDatHangDto, TimKiemCTDonDatHangDto>
    {
        public InvoiceDetailsController(ICTDonDatHangService ctDonHang) : base(ctDonHang)
        {
        }
    }
}
