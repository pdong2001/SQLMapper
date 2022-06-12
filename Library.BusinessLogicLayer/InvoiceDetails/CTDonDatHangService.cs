using AutoMapper;
using Library.BusinessLogicLayer.ProductDetails;
using Library.BusinessLogicLayer.Products;
using Library.Common.Dtos;
using Library.Common.Interfaces;
using Library.DataAccessLayer;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.InvoiceDetails
{
    public class CTDonDatHangService : BasicService<long, ChiTietDonDatHang, ChiTietDonDatHangDto, TimKiemCTDonDatHangDto>, ICTDonDatHangService
    {
        private readonly WebShopDbHelper _dbHepler;
        private readonly IChiTietDoUongService _ctSanPham;
        public CTDonDatHangService(WebShopDbHelper dbHepler, IMapper mapper, IChiTietDoUongService ctSanPham) : base(dbHepler.DSCTDonDatHang, mapper)
        {
            _dbHepler = dbHepler;
            _ctSanPham = ctSanPham;
        }

        public override PagedAndSortedResultDto<ChiTietDonDatHangDto> Pagination(TimKiemCTDonDatHangDto request)
        {
            var query = new List<DbQueryParameterGroup>();
            if (request.Invoice_Id.HasValue)// Lọc theo mã hóa đơn
            {
                query.Add(new DbQueryParameterGroup(new DbQueryParameter(nameof(ChiTietDonDatHang.Invoice_Id), request.Invoice_Id)));
            }
            var data = _data.Pagination(request, query.ToArray());
            var result = new PagedAndSortedResultDto<ChiTietDonDatHangDto>()
            {
                TotalPages = data.TotalPages,
                TotalRecords = data.TotalRecords,
                CurrentPage = data.CurrentPage,
                Items = data.Items.Select(i => mapper.Map<ChiTietDonDatHang, ChiTietDonDatHangDto>(i))
                .ToList(),
                PerPage = data.PerPage,
            };
            result.Items.ToList().ForEach(i =>
            {
                if (i.Product_Detail_id.HasValue)
                    i.Product_Detail = _ctSanPham.Find(i.Product_Detail_id.Value);
            });
            return result;
        }
    }
}
