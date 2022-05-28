using AutoMapper;
using Library.Common.Dtos;
using Library.DataAccessLayer;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Providers
{
    public class ProviderService : BasicService<long, Provider, ProviderDto, PageRequestDto>, IProviderService
    {
        private readonly WebShopDbHelper _dbHelper;

        public ProviderService(WebShopDbHelper dbHelper, IMapper mapper) : base(dbHelper.Providers, mapper)
        {
            _dbHelper = dbHelper;
        }
    }
}
