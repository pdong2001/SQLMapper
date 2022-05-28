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
        public string Name { get; set; }
        public string File_Path { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
