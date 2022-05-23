using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Blobs
{
    public class BlobDto : AuditedEntity<long>
    {
        public string Name { get; set; }
        public string File_Path { get; set; }
    }
}
