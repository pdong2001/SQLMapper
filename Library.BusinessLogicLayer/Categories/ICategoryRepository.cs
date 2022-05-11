using Library.Common.Dtos;
using System;
using System.Collections.Generic;
using Library.DataModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Categories
{
    public interface ICategoryRepository
    {
        Task<Category> Find(long id);
        Task<PagedAndSortedResultDto<Category>> Pagination(PagedAndSortedLookUpDto request);
        Task<Category> Create(Category entity);
        Task<bool> Delete(long id);
        Task<bool> Update(long id, Category entity);
        Task<IList<Category>> GetList(int? Count);
        Task<IList<CategoryDto>> GetWithProduct(int? count);
    }
}
