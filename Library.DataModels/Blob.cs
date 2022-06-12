using Library.Common.Attributes;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    [TableName("Blobs")]
    public class Blob : AuditedEntity<long>
    {
        public string Name { get; set; } // Tên file
        public string File_Path { get; set; } //File path là một chuỗi định danh unique
        public string ContentType { get; set; } //Loại nội dung của file
        public byte[] Content { get; set; } //Nội dung của file được lưu dưới dạng 1 chuỗi các byte
    }
}
