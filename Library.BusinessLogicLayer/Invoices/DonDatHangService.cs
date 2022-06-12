using AutoMapper;
using Library.Common.Dtos;
using Library.Common.Interfaces;
using Library.DataAccessLayer;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer.Invoices
{
    public class DonDatHangService : BasicService<long, DonDatHang, DonDatHangDto, TimKiemDonDatHangDto>, IDonDatHangService
    {
        private readonly WebShopDbHelper _dbHelper;
        public DonDatHangService(WebShopDbHelper dbHelper, IMapper mapper) : base(dbHelper.DSDonDatHang, mapper)
        {
            _dbHelper = dbHelper;
        }
        public override PagedAndSortedResultDto<DonDatHangDto> Pagination(TimKiemDonDatHangDto request)
        {
            var data = base.Pagination(request);
            data.Items.ToList().ForEach(i =>
            {
                if (request.With_Detail)
                {
                    i.details = _dbHelper.DSCTDonDatHang.GetList(null, new DbQueryParameterGroup(
                        new DbQueryParameter(nameof(ChiTietDonDatHang.Invoice_Id), i.Id)
                    ));
                }
            });
            return data;
        }

        public override DonDatHangDto Create(DonDatHang entity)
        {
            if (entity.Status < 0 || entity.Status > 4)
            {
                throw new Exception("Invalid Status");
            }
            return base.Create(entity);
        }

        public override bool Update(long id, DonDatHang entity)
        {
            var donDatHang = Find(id);
            if (donDatHang == null) return false;
            if (entity.Status < 0 || entity.Status > 4)
            {
                throw new Exception("Invalid Status");
            }
            var result = base.Update(id, entity);
            if (result) // Nếu update thành công
            {
                if (entity.Status != 3 && entity.Status == 3) // Từ chối đơn hàng
                {
                    var cmd = _dbHelper.Connection.CreateCommand();
                    var chiTietSanPhamTable = _dbHelper.DSCTSanPham.GetTableName(); // Tên bảng chi tiết sản phẩm
                    var ctDonDatHangTable = _dbHelper.DSCTDonDatHang.GetTableName(); // Tên bảng chi tiết đơn đặt hàng
                    cmd.CommandText = $"UPDATE [{chiTietSanPhamTable}] SET {nameof(ChiTietDoUong.Remaining_Quantity)} = {nameof(ChiTietDoUong.Remaining_Quantity)} + ISNULL((SELECT SUM({nameof(ChiTietDonDatHang.Quantity)}) FROM {ctDonDatHangTable} WHERE {ctDonDatHangTable}.{nameof(ChiTietDonDatHang.Product_Detail_id)} = {chiTietSanPhamTable}.id AND {nameof(ChiTietDonDatHang.Invoice_Id)} = @id),0) WHERE Id IN (SELECT {nameof(ChiTietDonDatHang.Product_Detail_id)} FROM {ctDonDatHangTable} WHERE {nameof(ChiTietDonDatHang.Invoice_Id)} = @id)"; // Hoàn trả số lượng của các chi tiết sản phẩm có trong đơn hàng
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                else if (entity.Status == 3 && entity.Status != 3) // Duyệt đơn đã từ chối
                {
                    var cmd = _dbHelper.Connection.CreateCommand();
                    var chiTietSanPhamTable = _dbHelper.DSCTSanPham.GetTableName(); // Tên bảng chi tiết sản phẩm
                    var ctDonDatHangTable = _dbHelper.DSCTDonDatHang.GetTableName(); // Tên bảng chi tiết đơn đặt hàng
                    cmd.CommandText = $"UPDATE [{chiTietSanPhamTable}] SET {nameof(ChiTietDoUong.Remaining_Quantity)} = {nameof(ChiTietDoUong.Remaining_Quantity)} - ISNULL((SELECT SUM({nameof(ChiTietDonDatHang.Quantity)}) FROM {ctDonDatHangTable} WHERE {ctDonDatHangTable}.{nameof(ChiTietDonDatHang.Product_Detail_id)} = {chiTietSanPhamTable}.id AND {nameof(ChiTietDonDatHang.Invoice_Id)} = @id),0) WHERE Id IN (SELECT {nameof(ChiTietDonDatHang.Product_Detail_id)} FROM {ctDonDatHangTable} WHERE {nameof(ChiTietDonDatHang.Invoice_Id)} = @id)"; // Giảm số lượng của các chi tiết sản phẩm có trong đơn hàng
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            return result;
        }
        public override bool Delete(long id)
        {
            var data = Find(id);
            if (data == null) return false;
            if (data.Status != 3)
            {
                var cmd = _dbHelper.Connection.CreateCommand();
                var chiTietSanPhamTable = _dbHelper.DSCTSanPham.GetTableName(); // Tên bảng chi tiết sản phẩm
                var ctDonDatHangTable = _dbHelper.DSCTDonDatHang.GetTableName(); // Tên bảng chi tiết đơn đặt hàng
                cmd.CommandText = $"UPDATE [{chiTietSanPhamTable}] SET {nameof(ChiTietDoUong.Remaining_Quantity)} = {nameof(ChiTietDoUong.Remaining_Quantity)} + ISNULL((SELECT SUM({nameof(ChiTietDonDatHang.Quantity)}) FROM {ctDonDatHangTable} WHERE {ctDonDatHangTable}.{nameof(ChiTietDonDatHang.Product_Detail_id)} = {chiTietSanPhamTable}.id AND {nameof(ChiTietDonDatHang.Invoice_Id)} = @id),0) WHERE Id IN (SELECT {nameof(ChiTietDonDatHang.Product_Detail_id)} FROM {ctDonDatHangTable} WHERE {nameof(ChiTietDonDatHang.Invoice_Id)} = @id)"; // Hoàn trả số lượng của các chi tiết sản phẩm có trong đơn hàng
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            var result = base.Delete(id);
            return result;
        }

        public long CreateWithDetail(DonDatHang entity, IList<ChiTietDonDatHang> details)
        {
            var data = Create(entity);
            if (data == null) return 0;
            details.ToList().ForEach(d =>
            {
                d.Invoice_Id = data.Id;
                d.Price = _dbHelper.DSCTSanPham.Find(d.Product_Detail_id).Out_Price;
                if (data.Status != 3)
                {
                    var cmd = _dbHelper.Connection.CreateCommand();
                    var chiTietSanPhamTable = _dbHelper.DSCTSanPham.GetTableName(); // Tên bảng chi tiết sản phẩm
                    var ctDonDatHangTable = _dbHelper.DSCTDonDatHang.GetTableName(); // Tên bảng chi tiết đơn đặt hàng
                    cmd.CommandText = $"UPDATE [{chiTietSanPhamTable}] SET {nameof(ChiTietDoUong.Remaining_Quantity)} = {nameof(ChiTietDoUong.Remaining_Quantity)} - @quantity WHERE Id = @id"; // Giảm số lượng của các chi tiết sản phẩm có trong đơn hàng
                    cmd.Parameters.AddWithValue("@id", data.Id);
                    cmd.Parameters.AddWithValue("@quantity", d.Quantity);
                    cmd.ExecuteNonQuery();
                }
                var result = _dbHelper.DSCTDonDatHang.Create(d);
                if (result != null && data.Status != 3)
                {
                    var cmd = _dbHelper.Connection.CreateCommand();
                    var chiTietSanPhamTable = _dbHelper.DSCTSanPham.GetTableName(); // Tên bảng chi tiết sản phẩm
                    cmd.CommandText = $"UPDATE [{chiTietSanPhamTable}] SET {nameof(ChiTietDoUong.Remaining_Quantity)} = {nameof(ChiTietDoUong.Remaining_Quantity)} - @quantity WHERE Id = @id"; // Giảm số lượng của các chi tiết sản phẩm có trong đơn hàng
                    cmd.Parameters.AddWithValue("@id", d.Product_Detail_id);
                    cmd.Parameters.AddWithValue("@quantity", d.Quantity);
                    cmd.ExecuteNonQuery();
                }
            });
            var cmd = _dbHelper.Connection.CreateCommand();
            var chiTietDoanDatHangTable = _dbHelper.DSCTDonDatHang.GetTableName(); // Tên bảng chi tiết đơn đặt hàng
            var donDatHangTable = _dbHelper.DSDonDatHang.GetTableName(); // Tên bảng đơn đặt hàng
            cmd.CommandText = $"UPDATE [{donDatHangTable}] SET {nameof(DonDatHang.Total)} = ISNULL((SELECT SUM(quantity * price) FROM {chiTietDoanDatHangTable} WHERE {nameof(ChiTietDonDatHang.Invoice_Id)} = @id),0) WHERE Id = @id"; // Giảm số lượng của các chi tiết sản phẩm có trong đơn hàng
            cmd.Parameters.AddWithValue("@id", data.Id);
            cmd.ExecuteNonQuery();
            return data.Id;
        }
    }
}
