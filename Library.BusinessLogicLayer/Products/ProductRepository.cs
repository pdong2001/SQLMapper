using Library.Common.Dtos;
using Library.DataAccessLayer;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _context;

        public ProductRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Product> Create(Product entity)
        {
            var t = Task<Product>.Run(() =>
            {
                return _context.Products.Create(entity);
            });
            await t;
            return t.Result;
        }

        public async Task<bool> Delete(long id)
        {
            var t = Task<bool>.Run(() =>
            {
                return _context.Products.Delete(id);
            });
            await t;
            return t.Result;
        }

        public async Task<Product> Find(long id)
        {
            var t = Task<Product>.Run(() =>
            {
                return _context.Products.Find(id);
            });
            await t;
            return t.Result;
        }

        public async Task<IList<Product>> GetList(int? Count)
        {
            var t = Task<Product>.Run(() =>
            {
                return _context.Products.GetList(Count);
            });
            await t;
            return t.Result;
        }

        public async Task<PagedAndSortedResultDto<Product>> Pagination(PagedAndSortedLookUpDto request)
        {
            var t = Task<PagedAndSortedResultDto<Product>>.Run(() =>
            {
                return _context.Products.Pagination(request);
            });
            await t;
            return t.Result;
        }

        public async Task<bool> Update(long id, Product entity)
        {
            var t = Task<bool>.Run(() =>
            {
                return _context.Products.Update(id, entity);
            });
            await t;
            return t.Result;
        }
    }
}
