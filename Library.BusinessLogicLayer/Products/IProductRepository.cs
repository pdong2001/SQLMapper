using Library.Common.Dtos;
using System;
using System.Collections.Generic;
using Library.DataModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Products
{
    public interface IProductRepository
    {
        Task<Product> Find(long id);
        Task<PagedAndSortedResultDto<Product>> Pagination(PagedAndSortedLookUpDto request);
        Task<Product> Create(Product entity);
        Task<bool> Delete(long id);
        Task<bool> Update(long id, Product entity);
        Task<IList<Product>> GetList(int? Count);
    }
}
