using AutoMapper;
using Library.BusinessLogicLayer.Blobs;
using Library.BusinessLogicLayer.ProductDetails;
using Library.Common.Dtos;
using Library.DataAccessLayer;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Products
{
    public class DoUongService : BasicService<long, DoUong, DoUongDto, TimKiemDoUongDto>, IDoUongService
    {
        private readonly WebShopDbHelper _dbHelper;
        private readonly IChiTietDoUongService _productDetails;
        private readonly IFileService _blobService;
        public DoUongService(WebShopDbHelper dbHelper, IMapper mapper, IChiTietDoUongService productDetails, IFileService blobService) : base(dbHelper.DSDoUong, mapper)
        {
            _dbHelper = dbHelper;
            _productDetails = productDetails;
            _blobService = blobService;
        }

        public override DoUongDto Find(long id)
        {
            var data = base.Find(id);
            if (data.Default_Image.HasValue)
            {
                data.Image = _blobService.Find(data.Default_Image.Value);
                if (data.Image != null) data.Image.Content = null;
            }
            data.Details = _productDetails.GetList(null, new DbQueryParameterGroup(new DbQueryParameter(nameof(ChiTietDoUong.Product_Id), data.Id, CompareOperator.Equal)));
            return data;
        }

        public override PagedAndSortedResultDto<DoUongDto> Pagination(TimKiemDoUongDto request)
        {
            var query = new List<DbQueryParameterGroup>();
            if (request.category.HasValue)// Lọc theo loại sản phẩm
            {
                query.Add(new DbQueryParameterGroup(new DbQueryParameter(nameof(DoUong.Category_Id), request.category)));
            }
            if (request.visible_only)// Lọc theo trạng thái hiển thị
            {
                query.Add(new DbQueryParameterGroup(new DbQueryParameter(nameof(DoUong.Visible), true)));
            }
            var rawData = _data.Pagination(request, query.ToArray());
            var data = new PagedAndSortedResultDto<DoUongDto>()
            {
                TotalPages = rawData.TotalPages,
                TotalRecords = rawData.TotalRecords,
                CurrentPage = rawData.CurrentPage,
                Items = rawData.Items.Select(i => mapper.Map<DoUong, DoUongDto>(i)).ToList(),
                PerPage = rawData.PerPage,
            };
            data.Items.ToList().ForEach(item =>
            {
                if (request.With_Detail)
                {
                    item.Details = _productDetails.GetList(null, new DbQueryParameterGroup(new DbQueryParameter(nameof(ChiTietDoUong.Product_Id), item.Id, CompareOperator.Equal)));
                }
                if (item.Default_Image.HasValue)
                {
                    item.Image = _blobService.Find(item.Default_Image.Value);
                    if (item.Image != null) item.Image.Content = null;
                }
                if (item.Category_Id.HasValue)
                    item.Category = _dbHelper.DSLoaiDoUong.Find(item.Category_Id.Value);
            });
            return data;
        }
    }
}
