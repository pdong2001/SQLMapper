using Library.Common.Attributes;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    [TableName("DSLoaiSanPham")] //Tên bảng sẽ được tạo trong database
    public class LoaiSanPham : AuditedEntity<long> // Kế thừa AuditedEntity<long> sẽ có sẵn id có kiểu long, thời gian tạo, thời gian sửa.
    {
        // Các thuộc tính

        public string Name { get; set; }
        public bool Visible { get; set; }
        public string Note { get; set; }
        [ForeignKey(typeof(Blob))]
        public long? Blob_Id { get;set; }
    }
}
