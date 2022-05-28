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
        public int Page { get; set; } = 1;
        /// <summary>
        /// Kích thước, số phần tử
        /// </summary>
        public int Limit { get; set; } = 10;
        /// <summary>
        /// Cột để sắp xếp
        /// </summary>
        public string Column { get; set; } = "Id";
        /// <summary>
        /// Kiểu sắp xếp
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// Giả trị để tìm kiếm
        /// </summary>
        public string Search { get; set; }
    }
}
