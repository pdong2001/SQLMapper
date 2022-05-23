using AutoMapper;
using Library.Common.Dtos;
using Library.DataAccessLayer;
using Library.DataModels;

namespace Library.BusinessLogicLayer.Providers
{
    public class ProviderService : BasicService<long, Provider, ProviderDto, PageRequestDto>, IProviderService
    {
        private readonly WebShopDbHelper _dbHelper;
        public ProviderService(IMapper mapper, WebShopDbHelper webShopDbHelper) : base(webShopDbHelper.Providers, mapper)
        {
            this._dbHelper = webShopDbHelper;
        }

    }
}
