using System;
using Library.DataAccessLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Migrate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            WebShopDbHelper databaseContext = new WebShopDbHelper(config.GetConnectionString("Default"));
            databaseContext.CreateTablesIfNotExsist();
        }
    }
}
