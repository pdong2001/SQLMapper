using AutoMapper;
using Library.Common.Dtos;
using Library.DataAccessLayer;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Categories
{
    public class LoaiDoUongService : BasicService<long, LoaiDoUong, LoaiDoUongDto, PageRequestDto>, ILoaiDoUongService
    {
        private readonly WebShopDbHelper _dbHelper;

        public LoaiDoUongService(WebShopDbHelper dbHelper, IMapper mapper) : base(dbHelper.DSLoaiDoUong, mapper)
        {
            _dbHelper = dbHelper;
        }
    }
}
