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
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            DatabaseContext databaseContext = new DatabaseContext(config.GetConnectionString("Default"));
            databaseContext.CreateTablesIfNotExsist();
        }
    }
}
