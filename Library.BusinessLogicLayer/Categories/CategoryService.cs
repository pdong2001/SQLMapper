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
    public class CategoryService : BasicService<long, Category, CategoryDto, PageRequestDto>, ICategoryService
    {
        private readonly WebShopDbHelper _dbHelper;

        public CategoryService(WebShopDbHelper dbHelper, IMapper mapper) : base(dbHelper.Categories, mapper)
        {
            _dbHelper = dbHelper;
        }
    }
}
