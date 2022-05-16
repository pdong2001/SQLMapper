using Library.BusinessLogicLayer.Invoices;
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
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        /// <summary>
        /// Lấy danh sách
        /// </summary>
        /// <param name="count">Số phần tử tối đa</param>
        /// <returns>Danh sách hóa đơn</returns>
        [HttpGet]
        [Route("all")]
        public IActionResult GetList([FromQuery] int? count)
        {
            return Ok(_invoiceService.GetList(count));
        }

        /// <summary>
        /// Tạo hóa đơn
        /// </summary>
        /// <param name="input">Thông tin hóa đơn</param>
        /// <returns>Hóa đơn vừa tạo</returns>
        [HttpPost]
        public IActionResult Create([FromBody] Invoice input)
        {
            return Ok(_invoiceService.Create(input));
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] long id,[FromBody] Invoice input)
        {
            return Ok(_invoiceService.Update(id, input));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody] long id)
        {
            return Ok(_invoiceService.Delete(id));
        }

        [HttpGet]
        public IActionResult Paginate([FromQuery] PageRequestDto request)
        {
            return Ok(_invoiceService.Pagination(request));
        }
    }
}
