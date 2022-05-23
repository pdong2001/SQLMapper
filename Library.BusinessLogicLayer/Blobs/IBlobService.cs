using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Blobs
{
    public interface IBlobService
    {
        Task<BlobDto> GetById(long id);
    }
}
