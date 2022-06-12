using AutoMapper;
using Library.BusinessLogicLayer.Blobs;
using Library.BusinessLogicLayer.Products;
using Library.Common.Dtos;
using Library.DataAccessLayer;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.ProductDetails
{
    public class ChiTietDoUongService : BasicService<long, ChiTietDoUong, ChiTietSanPhamDto, TimKiemCTDoUongDto>, IChiTietDoUongService
    {
        private readonly WebShopDbHelper _dbHelper;
        private readonly IFileService _blobService;

        public ChiTietDoUongService(WebShopDbHelper dbHelper, IMapper mapper, IFileService blobService) : base(dbHelper.DSCTSanPham, mapper)
        {
            _dbHelper = dbHelper;
            _blobService = blobService;
        }

        public override ChiTietSanPhamDto Find(long id)
        {
            var data = base.Find(id);
            data.Product = mapper.Map<DoUong, DoUongDto>(_dbHelper.DSDoUong.Find(data.Product_Id));
            if (data.Product != null && data.Product.Default_Image.HasValue)
            {
                data.Product.Image = _dbHelper.Blobs.Find(data.Default_Image.Value);
                if (data.Product.Image != null) data.Product.Image.Content = null;
            }
            if (data.Default_Image.HasValue)
            {
                data.Image = _dbHelper.Blobs.Find(data.Default_Image.Value);
                if (data.Image != null) data.Image.Content = null;
            }
            return data;
        }

        public override PagedAndSortedResultDto<ChiTietSanPhamDto> Pagination(TimKiemCTDoUongDto request)
        {
            var query = new List<DbQueryParameterGroup>();
            if (request.product_id.HasValue)// Lọc theo loại sản phẩm
            {
                query.Add(new DbQueryParameterGroup(new DbQueryParameter(nameof(ChiTietDoUong.Product_Id), request.product_id)));
            }
            if (request.consumable_only)// Lọc theo số lượng còn lại
            {
                query.Add(new DbQueryParameterGroup(new DbQueryParameter(nameof(ChiTietDoUong.Remaining_Quantity), 0, CompareOperator.GreaterThan)));
            }
            var rawData = _data.Pagination(request, query.ToArray());
            var data = new PagedAndSortedResultDto<ChiTietSanPhamDto>()
            {
                TotalPages = rawData.TotalPages,
                TotalRecords = rawData.TotalRecords,
                CurrentPage = rawData.CurrentPage,
                Items = rawData.Items.Select(i => mapper.Map<ChiTietDoUong, ChiTietSanPhamDto>(i)).ToList(),
                PerPage = rawData.PerPage,
            };
            data.Items.ToList().ForEach(item =>
            {
                if (request.With_Product)
                {
                    item.Product = mapper.Map<DoUong, DoUongDto>(_dbHelper.DSDoUong.Find(item.Product_Id));
                }
                if (item.Default_Image.HasValue)
                {
                    item.Image = _blobService.Find(item.Default_Image.Value);
                    if (item.Image != null) item.Image.Content = null;
                }
            });
            return data;
        }
    }
}
