using Library.Common.Dtos;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Blobs
{
    public interface IFileService : IBasicService<long, Blob, BlobDto, PageRequestDto>
    {
        public BlobDto FindPath(string name);
    }
}
