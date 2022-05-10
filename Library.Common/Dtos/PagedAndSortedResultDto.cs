using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Dtos
{
    public class PagedAndSortedResultDto<T>
    {
        public int TotalRecords { get; set; } = 0;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 0;
        public int PerPage { get; set; } = 10;
        public IList<T> Items { get; set; }
    }
}
