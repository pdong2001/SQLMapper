using Library.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Interfaces
{
    public interface IEntitySet
    {

    }
    public interface IEntitySet<T> : IEntitySet
    {
        T Find(object id);
        PagedAndSortedResultDto<T> Pagination(PageRequestDto request);
        T Create(T entity);
        bool Delete(object id);
        bool Update(object id, T entity);
        IList<T> GetList(int? Count = null, params DbQueryParameter[] dbQueryParameters);
        void CreateTableIfNotExists();
        void AddConstraints();
    }
}
