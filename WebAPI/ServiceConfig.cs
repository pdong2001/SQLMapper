
using Library.BusinessLogicLayer.Blobs;
using Library.BusinessLogicLayer.Categories;
using Library.BusinessLogicLayer.InvoiceDetails;
using Library.BusinessLogicLayer.Invoices;
using Library.BusinessLogicLayer.ProductDetails;
using Library.BusinessLogicLayer.Products;
using Library.Common;
using Library.Common.Interfaces;
using Library.DataAccessLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;

namespace WebAPI
{
    public static class ServiceConfig
    {
        public static DateTime ExpiresTimes => DateTime.UtcNow.AddDays(1);
        public static void AddServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IJWTManagerRepository, JWTManagerRepository>(s => 
                new JWTManagerRepository(Configuration, s.GetRequiredService<IDatabaseHelper>(), ExpiresTimes));
            services.AddScoped<IDatabaseHelper, WebShopDbHelper>(services =>
            {
                return new WebShopDbHelper(Configuration.GetConnectionString("Default"));
            });
            services.AddScoped(services =>
            {
                return new WebShopDbHelper(Configuration.GetConnectionString("Default"));
            });
            services.AddScoped<ILoaiSanPhamService, LoaiSanPhamService>();
            services.AddScoped<ISanPhamService, SanPhamService>();
            services.AddScoped<IChiTietSanPhamService, ChiTietSanPhamService>();
            services.AddScoped<IDonDatHangService, DonDatHangService>();
            services.AddScoped<ICTDonDatHangService, CTDonDatHangService>();
            services.AddScoped<IFileService, FileService>();
        }
    }
    public class LowerCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToLower();
    }
}
