using Library.Common.Attributes;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    [TableName("DSSanPham")]
    public class SanPham : AuditedEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public bool Visible { get; set; } = true;
        public int Option_Count { get; set; } = 0;

        [ForeignKey(typeof(Blob))]
        public long? Default_Image { get; set; }

        [ForeignKey(typeof(LoaiSanPham))]
        public long? Category_Id { get; set; }
    }
}
