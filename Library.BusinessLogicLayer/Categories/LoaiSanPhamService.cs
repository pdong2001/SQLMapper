using AutoMapper;
using Library.Common.Dtos;
using Library.DataAccessLayer;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Categories
{
    public class LoaiSanPhamService : BasicService<long, LoaiSanPham, LoaiSanPhamDto, PageRequestDto>, ILoaiSanPhamService
    {
        private readonly WebShopDbHelper _dbHelper;

        public LoaiSanPhamService(WebShopDbHelper dbHelper, IMapper mapper) : base(dbHelper.DSLoaiSanPham, mapper)
        {
            _dbHelper = dbHelper;
        }

        public override PagedAndSortedResultDto<LoaiSanPhamDto> Pagination(PageRequestDto request)
        {
            var data = base.Pagination(request);
            data.Items.Where(item => item.Blob_Id.HasValue).ToList().ForEach(item =>
            {
                item.Image = _dbHelper.Blobs.Find(item.Blob_Id.Value);
            });
            return data;
        }
    }
}
