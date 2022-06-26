using Library.Common.Attributes;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    [TableName("DonDatHang")]
    public class DonDatHang : AuditedEntity<long>
    {
        public static string TenTrangThai(int MaTrangThai)
        {
            switch (MaTrangThai)
            {
                case 0:
                    return "Đang duyệt";
                case 1:
                    return "Đã duyệt";
                case 2:
                    return "Đang giao";
                case 3:
                    return "Từ chối";
                case 4:
                    return "Hoàn thành";
                default:
                    return "Đang duyệt";
            }
        }
        public long Total { get; set; }
        public long Paid { get; set; }
        [Required]
        public string Customer_Name { get; set; }
        public string Phone_Number { get; set; }
        [Required]
        public string Address { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
