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
        /// <summary>
        /// Số trang
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// Kích thước, số phần tử
        /// </summary>
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// Cột để sắp xếp
        /// </summary>
        public string Column { get; set; } = "Id";
        /// <summary>
        /// Kiểu sắp xếp
        /// </summary>
        public SortOrder SortOrder { get; set; } = SortOrder.DESC;
        /// <summary>
        /// Giả trị để tìm kiếm
        /// </summary>
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
