using AutoMapper;
using Library.Common.Dtos;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer
{
    public class BasicService<TKey, TEntity, TDto, LookUpDto> : IBasicService<TKey, TEntity, TDto, LookUpDto> where TKey : struct where LookUpDto : PageRequestDto
    {
        protected readonly IEntitySet<TEntity> _data;
        protected readonly IMapper mapper;
        public BasicService(IEntitySet<TEntity> data, IMapper mapper)
        {
            _data = data;
            this.mapper = mapper;
        }

        public virtual TDto Create(TEntity entity)
        {
            var data = _data.Create(entity);
            return data != null ? mapper.Map<TEntity, TDto>(data) : default(TDto);
        }

        public virtual Task<TDto> CreateAsync(TEntity entity)
        {
            return Task.Run(() =>
            {
                var data = _data.Create(entity);
                return data != null ? mapper.Map<TEntity, TDto>(data) : default(TDto);
            });
        }

        public virtual bool Delete(TKey id)
        {
            return _data.Delete(id);
        }

        public virtual Task<bool> DeleteAsync(TKey id)
        {
            return Task.Run(() =>
            {
                return _data.Delete(id);
            });
        }

        public virtual TDto Find(TKey id)
        {
            var data = _data.Find(id);
            return data != null ? mapper.Map<TEntity, TDto>(data) : default(TDto);
        }

        public virtual Task<TDto> FindAsync(TKey id)
        {
            return Task.Run(() =>
            {
                var data = _data.Find(id);
                return data != null ? mapper.Map<TEntity, TDto>(data) : default(TDto);
            });
        }

        public virtual IList<TDto> GetList(int? Count, params DbQueryParameter[] dbQuerys)
        {
            return _data.GetList(Count, dbQuerys).Select(source => mapper.Map<TEntity, TDto>(source)).ToList();
        }

        public virtual async Task<IList<TDto>> GetListAsync(int? Count, params DbQueryParameter[] dbQuerys)
        {
            return await Task.Run(() =>
            {
                return _data.GetList(Count, dbQuerys).Select(source => mapper.Map<TEntity, TDto>(source)).ToList();
            });
        }

        public virtual PagedAndSortedResultDto<TDto> Pagination(LookUpDto request)
        {
            return mapper.Map<PagedAndSortedResultDto<TEntity>, PagedAndSortedResultDto<TDto>>(_data.Pagination(request));
        }

        public virtual Task<PagedAndSortedResultDto<TDto>> PaginationAsync(LookUpDto request)
        {
            return Task.Run(() =>
            {
                return mapper.Map<PagedAndSortedResultDto<TEntity>, PagedAndSortedResultDto<TDto>>(_data.Pagination(request));
            });
        }

        public virtual bool Update(TKey id, TEntity entity)
        {
            return _data.Update(id, entity);
        }

        public virtual Task<bool> UpdateAsync(TKey id, TEntity entity)
        {
            return Task.Run(() =>
            {
                return _data.Update(id, entity);
            });
        }
    }
}
