using Library.Common.Attributes;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    [TableName("ImageAssigns")]
    public class ImageAssign : Entity<long>
    {
        [ForeignKey(typeof(Blob))]
        public long Blob_Id { get; set; }
        public string Imageable_Type { get; set; }
        public long Imageable_Id { get; set; }
    }
}
