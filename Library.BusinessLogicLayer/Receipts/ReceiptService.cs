using AutoMapper;
using Library.Common.Dtos;
using Library.DataAccessLayer;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Receipts
{
    public class ReceiptService : BasicService<long, Receipt, ReceiptDto, PageRequestDto>, IReceiptService
    {
        private readonly WebShopDbHelper _dbHelper;

        public ReceiptService(WebShopDbHelper dbHelper, IMapper mapper) : base(dbHelper.Receipts, mapper)
        {
            _dbHelper = dbHelper;
        }
    }
}
