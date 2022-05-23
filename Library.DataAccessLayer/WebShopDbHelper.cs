using Library.Common;
using System;

namespace Library.DataAccessLayer
{
    public class WebShopDbHelper : DatabaseHelper
    {
        public WebShopDbHelper(string connectionString) : base(connectionString)
        {
        }

    }
}
