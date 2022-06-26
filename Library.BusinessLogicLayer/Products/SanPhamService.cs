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
    public class SanPhamService : BasicService<long, SanPham, SanPhamDto, TimKiemSanPhamDto>, ISanPhamService
    {
        private readonly WebShopDbHelper _dbHelper;
        private readonly IChiTietSanPhamService _productDetails;
        private readonly IFileService _blobService;
        public SanPhamService(WebShopDbHelper dbHelper, IMapper mapper, IChiTietSanPhamService productDetails, IFileService blobService) : base(dbHelper.DSSanPham, mapper)
        {
            _dbHelper = dbHelper;
            _productDetails = productDetails;
            _blobService = blobService;
        }

        public override SanPhamDto Find(long id)
        {
            var data = base.Find(id);
            if (data.Default_Image.HasValue)
            {
                data.Image = _blobService.Find(data.Default_Image.Value);
                if (data.Image != null) data.Image.Content = null;
            }
            data.Details = _productDetails.GetList(null, new DbQueryParameterGroup(new DbQueryParameter(nameof(ChiTietSanPham.Product_Id), data.Id, CompareOperator.Equal)));
            data.Details.Where(i => i.Default_Image.HasValue).ToList().ForEach(i =>
            {
                i.Image = _blobService.Find(i.Default_Image.Value);
            });
            if (data.Category_Id.HasValue)
            {
                data.Category = _dbHelper.DSLoaiSanPham.Find(data.Category_Id.Value);
            }
            data.Min_Price = data.Details.Min(d => d.Out_Price);
            data.Default_Detail = data.Details.FirstOrDefault(d => d.Out_Price == data.Min_Price);
            return data;
        }

        public override PagedAndSortedResultDto<SanPhamDto> Pagination(TimKiemSanPhamDto request)
        {
            var query = new List<DbQueryParameterGroup>();
            if (request.category.HasValue)// Lọc theo loại sản phẩm
            {
                query.Add(new DbQueryParameterGroup(new DbQueryParameter(nameof(SanPham.Category_Id), request.category)));
            }
            if (request.visible_only)// Lọc theo trạng thái hiển thị
            {
                query.Add(new DbQueryParameterGroup(new DbQueryParameter(nameof(SanPham.Visible), true)));
            }
            var rawData = _data.Pagination(request, query.ToArray());
            var data = new PagedAndSortedResultDto<SanPhamDto>()
            {
                TotalPages = rawData.TotalPages,
                TotalRecords = rawData.TotalRecords,
                CurrentPage = rawData.CurrentPage,
                Items = rawData.Items.Select(i => mapper.Map<SanPham, SanPhamDto>(i)).ToList(),
                PerPage = rawData.PerPage,
            };
            data.Items.ToList().ForEach(item =>
            {
                var cmd = _dbHelper.Connection.CreateCommand();
                cmd.CommandText = $"SELECT ISNULL(MIN({nameof(ChiTietSanPham.Out_Price)}),0) FROM {_dbHelper.DSCTSanPham.GetTableName()} WHERE {nameof(ChiTietSanPham.Product_Id)} = @id";
                cmd.Parameters.AddWithValue("id", item.Id);
                item.Min_Price = (int)cmd.ExecuteScalar();

                    item.Details = _productDetails.GetList(null, new DbQueryParameterGroup(new DbQueryParameter(nameof(ChiTietSanPham.Product_Id), item.Id, CompareOperator.Equal)));
                    item.Details.Where(i => i.Default_Image.HasValue).ToList().ForEach(i =>
                    {
                        i.Image = _blobService.Find(i.Default_Image.Value);
                    });
                    item.Default_Detail = item.Details.FirstOrDefault(d => d.Out_Price == item.Min_Price);

                if (item.Default_Image.HasValue)
                {
                    item.Image = _blobService.Find(item.Default_Image.Value);
                    if (item.Image != null) item.Image.Content = null;
                }
                if (item.Category_Id.HasValue)
                    item.Category = _dbHelper.DSLoaiSanPham.Find(item.Category_Id.Value);
            });
            return data;
        }
    }
}
