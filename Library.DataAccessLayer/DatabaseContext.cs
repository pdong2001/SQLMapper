using Library.Common;
using Library.DataAccessLayer.EntitySets;
using Library.DataModels;
using System;

namespace Library.DataAccessLayer
{
    public class DatabaseContext : DatabaseHelper
    {
        public DatabaseContext(string connectionString) : base(connectionString)
        {
        }
        public ProductSet Products { get; set; }
        public CategorySet Categories { get; set; }
    }
}
