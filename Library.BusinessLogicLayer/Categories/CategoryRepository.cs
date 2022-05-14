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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WebShopDbHelper _context;
        private readonly IMapper mapper;

        public CategoryRepository(WebShopDbHelper context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<Category> Create(Category entity)
        {
            var t = Task<Category>.Run(() =>
            {
                return _context.Categories.Create(entity);
            });
            await t;
            return t.Result;
        }

        public async Task<bool> Delete(long id)
        {
            var t = Task<bool>.Run(() =>
            {
                return _context.Categories.Delete(id);
            });
            await t;
            return t.Result;
        }

        public async Task<Category> Find(long id)
        {
            var t = Task<Category>.Run(() =>
            {
                return _context.Categories.Find(id);
            });
            await t;
            return t.Result;
        }

        public async Task<IList<Category>> GetList(int? Count)
        {
            var t = Task<Category>.Run(() =>
            {
                return _context.Categories.GetList(Count
                    //, new DbQueryParameter
                    //{
                    //    Name = "Name",
                    //    CompareOperator = CompareOperator.Like,
                    //    Value = "%Loại%",
                    //    LogicOperator = LogicOperator.OR
                    //},
                    //new DbQueryParameter
                    //{
                    //    Name = "Name",
                    //    CompareOperator = CompareOperator.Same,
                    //    Value = "string",
                    //    LogicOperator = LogicOperator.OR
                    //}
                    );
            });
            await t;
            return t.Result;
        }

        public async Task<PagedAndSortedResultDto<Category>> Pagination(PageRequestDto request)
        {
            var t = Task<PagedAndSortedResultDto<Category>>.Run(() =>
            {
                return _context.Categories.Pagination(request);
            });
            await t;
            return t.Result;
        }

        public async Task<bool> Update(long id, Category entity)
        {
            var t = Task<bool>.Run(() =>
            {
                return _context.Categories.Update(id, entity);
            });
            await t;
            return t.Result;
        }

        public async Task<IList<CategoryDto>> GetWithProduct(int? count)
        {
            var task = new Task<IList<CategoryDto>>(() =>
            {
                var data = _context.Categories.GetList(count);
                var result = data.Select(cate =>
                {
                    var cateDto = mapper.Map<Category, CategoryDto>(cate);
                    cateDto.Products = _context.Products.GetList(null, new DbQueryParameter { Name = nameof(Product.Category_Id), CompareOperator = CompareOperator.Same, Value = cateDto.Id });
                    return cateDto;
                }).ToList();
                return result;
            });
            task.Start();
            await task;
            return task.Result;
        }
    }
}
