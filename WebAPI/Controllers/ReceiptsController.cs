using Library.BusinessLogicLayer.Receipts;
using Library.Common.Dtos;
using Library.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ReceiptsController : BaseAPIController<long, Receipt, ReceiptDto, PageRequestDto>
    {
        public ReceiptsController(IReceiptService receipts) : base(receipts)
        {
        }
    }
}
