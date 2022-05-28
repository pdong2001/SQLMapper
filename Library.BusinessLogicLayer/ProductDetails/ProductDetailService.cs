using AutoMapper;
using Library.BusinessLogicLayer.Blobs;
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
    public class ProductDetailService : BasicService<long, ProductDetail, ProductDetailDto, PageRequestDto>, IProductDetailService
    {
        private readonly WebShopDbHelper _dbHelper;
        private readonly IBlobService _blobService;

        public ProductDetailService(WebShopDbHelper dbHelper, IMapper mapper, IBlobService blobService) : base(dbHelper.ProductDetails, mapper)
        {
            _dbHelper = dbHelper;
            _blobService = blobService;
        }

        public override PagedAndSortedResultDto<ProductDetailDto> Pagination(PageRequestDto request)
        {
            var data = base.Pagination(request);
            data.Items.ToList().ForEach(item =>
            {
                item.Product = _dbHelper.Products.Find(item.Product_Id);
                if (item.Default_Image.HasValue)
                    item.Image = _blobService.Find(item.Default_Image.Value);
            });
            return data;
        }

        public override ProductDetailDto Create(ProductDetail entity)
        {
            var data = base.Create(entity);
            if (data != null)
            {
                var cmd = _dbHelper.Connection.CreateCommand();
                var productTable = _dbHelper.Products.GetTableName();
                var quantityColumn = nameof(Product.Quantity);
                cmd.CommandText = $"UPDATE [{productTable}] SET [{quantityColumn}] = [{quantityColumn}] + @quantity WHERE Id = @id";
                cmd.Parameters.AddWithValue("@quantity", data.Remaining_Quantity);
                cmd.Parameters.AddWithValue("@id", data.Product_Id);
                cmd.ExecuteNonQuery();
            }
            return data;
        }

        public override bool Delete(long id)
        {
            var data = _data.Find(id);
            var result = base.Delete(id);
            if (data != null && result)
            {
                var cmd = _dbHelper.Connection.CreateCommand();
                var productTable = _dbHelper.Products.GetTableName();
                var quantityColumn = nameof(Product.Quantity);
                cmd.CommandText = $"UPDATE [{productTable}] SET [{quantityColumn}] = [{quantityColumn}] - @quantity WHERE Id = @id";
                cmd.Parameters.AddWithValue("@quantity", data.Remaining_Quantity);
                cmd.Parameters.AddWithValue("@id", data.Product_Id);
                cmd.ExecuteNonQuery();
            }
            return result;
        }

        public override bool Update(long id, ProductDetail entity)
        {
            var data = _data.Find(id);
            var result = base.Update(id, entity);
            if (data != null && result)
            {
                var cmd = _dbHelper.Connection.CreateCommand();
                var productTable = _dbHelper.Products.GetTableName();
                var productDetailTable = _dbHelper.ProductDetails.GetTableName();
                var quantityColumn = nameof(Product.Quantity);
                var remainQuantityColumn = nameof(ProductDetail.Remaining_Quantity);
                cmd.CommandText = $"UPDATE [{productTable}] SET [{quantityColumn}] = (SELECT SUM([{productDetailTable}].[{remainQuantityColumn}]) FROM [{productDetailTable}] WHERE [{productDetailTable}].[{nameof(ProductDetail.Product_Id)}] = @id) WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", data.Product_Id);
                cmd.ExecuteNonQuery();
            }
            return result;
        }
    }
}
