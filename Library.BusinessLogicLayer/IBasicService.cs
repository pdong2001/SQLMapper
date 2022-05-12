using Library.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer
{
    public interface IBasicService<TKey, Entity, Dto, LookUpDto> where TKey : struct where LookUpDto : PageRequestDto
    {
        Task<Dto> FindAsync(TKey id);
        Task<PagedAndSortedResultDto<Dto>> PaginationAsync(LookUpDto request);
        Task<Dto> CreateAsync(Entity entity);
        Task<bool> DeleteAsync(TKey id);
        Task<bool> UpdateAsync(TKey id, Entity entity);
        Task<IList<Dto>> GetListAsync(int? Count, params DbQueryParameter[] dbQuerys);

        Dto Find(TKey id);
        PagedAndSortedResultDto<Dto> Pagination(LookUpDto request);
        Dto Create(Entity entity);
        bool Delete(TKey id);
        bool Update(TKey id, Entity entity);
        IList<Dto> GetList(int? Count, params DbQueryParameter[] dbQuerys);
    }
}
