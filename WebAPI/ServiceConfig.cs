
using Library.Common;
using Library.Common.Interfaces;
using Library.DataAccessLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using System.Text.Json;

namespace WebAPI
{
    public static class ServiceConfig
    {
        public static DateTime ExpiresTimes => DateTime.UtcNow.AddSeconds(1);
        public static void AddServices(this IServiceCollection services, IConfiguration Configuration)
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
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key),
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddScoped<IJWTManagerRepository, JWTManagerRepository>();
            services.AddScoped<IDatabaseHelper, WebShopDbHelper>(services =>
            {
                return new WebShopDbHelper(Configuration.GetConnectionString("Default"));
            });
            services.AddScoped(services =>
            {
                return new WebShopDbHelper(Configuration.GetConnectionString("Default"));
            });
        }
    }
    public class LowerCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToLower();
    }
}
