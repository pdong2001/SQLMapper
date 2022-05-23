using AutoMapper;
using Library.Common.Dtos;
using Library.Common.Interfaces;
using Library.DataAccessLayer;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Categories
{
    public class CategoryService : BasicService<long, Category, CategoryDto, PageRequestDto>, ICategoryService
    {
        private readonly WebShopDbHelper webShopDbHelper;
        public CategoryService(IMapper mapper, WebShopDbHelper webShopDbHelper) : base(webShopDbHelper.Categories, mapper)
        {
            this.webShopDbHelper = webShopDbHelper;
        }
    }
}
