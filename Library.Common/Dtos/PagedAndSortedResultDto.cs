using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Dtos
{
    public class PagedAndSortedResultDto<T> : IPagedAndSortedResultDto
    {
        public int TotalRecords { get; set; } = 0;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 0;
        public int PerPage { get; set; } = 10;
        public IList<T> Items { get; set; }
    }
    public interface IPagedAndSortedResultDto
    {
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PerPage { get; set; }
    }
}
