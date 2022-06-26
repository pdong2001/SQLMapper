using Library.BusinessLogicLayer.InvoiceDetails;
using Library.Common.Dtos;
using Library.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [Obsolete]
        [NonAction]
        public override IActionResult Create([FromBody] ChiTietDonDatHang product)
        {
            return base.Create(product);
        }
        [Obsolete]
        [NonAction]
        public override IActionResult Edit([FromRoute] long id, [FromBody] ChiTietDonDatHang product)
        {
            return base.Edit(id, product);
        }
        [Obsolete]
        [NonAction]
        public override IActionResult Delete([FromRoute] long id)
        {
            return base.Delete(id);
        }
    }
}
