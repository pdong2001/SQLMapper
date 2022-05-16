using Library.BusinessLogicLayer.ProductCategories;
using Library.BusinessLogicLayer.Products;
using Library.Common.Interfaces;
using Library.DataAccessLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = new LowerCaseNamingPolicy();
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WebAPI",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Email = @"phamphuongdong2001@gmail.com",
                        Name = "Phạm Phương Đông",
                        Url = new Uri(@"https://www.facebook.com/profile.php?id=100014223942428"),
                    },
                    Description = "Thư viện được thực hiện bởi Phạm Phương Đông",
                    License = new OpenApiLicense()
                    {
                        Name = "Phạm Phương Đông",
                        Url = new Uri(@"https://www.facebook.com/profile.php?id=100014223942428")
                    }
                });
            });

            services.AddScoped(services =>
            {
                return new WebShopDbHelper(Configuration.GetConnectionString("Default"));
            });
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public class LowerCaseNamingPolicy : JsonNamingPolicy
        {
            public override string ConvertName(string name) => name.ToLower();
        }
    }
}
