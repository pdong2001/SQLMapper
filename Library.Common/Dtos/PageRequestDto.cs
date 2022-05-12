using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Dtos
{
    public class PageRequestDto
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Column { get; set; } = "Id";
        public SortOrder SortOrder { get; set; } = SortOrder.DESC;
        public string Search { get; set; }
    }

    public enum SortOrder
    {
        /// <summary>
        /// Tăng dần
        /// </summary>
        [Description("ASC")]
        ASC,

        /// <summary>
        /// Giảm dần
        /// </summary>
        [Description("DESC")]
        DESC
    }
}
