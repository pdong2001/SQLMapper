using AutoMapper;
using Library.BusinessLogicLayer.Blobs;
using Library.BusinessLogicLayer.Categories;
using Library.BusinessLogicLayer.InvoiceDetails;
using Library.BusinessLogicLayer.Invoices;
using Library.BusinessLogicLayer.ProductDetails;
using Library.BusinessLogicLayer.Products;
using Library.Common.Dtos;
using Library.DataModels;

namespace WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<LoaiSanPham, LoaiSanPhamDto>();
            CreateMap<SanPham,SanPhamDto>();
            CreateMap<ChiTietSanPham, ChiTietSanPhamDto>();
            CreateMap<Blob, BlobDto>();
            CreateMap<ChiTietDonDatHang, ChiTietDonDatHangDto>();
            CreateMap<DonDatHang, DonDatHangDto>();
        }
    }
}
