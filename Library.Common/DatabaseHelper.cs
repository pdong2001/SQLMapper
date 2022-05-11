using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Library.Common
{
    public abstract class DatabaseHelper : IDatabaseHelper, IDisposable
    {
        private readonly SqlConnection _connection;
        public DatabaseHelper(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            var sets = GetType().GetProperties().Where(t => t.PropertyType.GetInterface(typeof(IEntitySet).Name) != null);
            foreach (var set in sets)
            {
                set.SetValue(this, set.PropertyType.GetConstructors()[0].Invoke(new object[] { _connection }));
            }
            CreateTablesIfNotExsist();
        }

        public void CreateTablesIfNotExsist()
        {
            var sets = GetType().GetProperties().Where(t => t.PropertyType.GetInterface(typeof(IEntitySet).Name) != null);
            foreach (var set in sets)
            {
                set.PropertyType.GetMethod("CreateTableIfNotExists").Invoke(set.GetValue(this),null);
            }
        }

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
        }

        public void DropAllTable()
        {
            throw new NotImplementedException();
        }
    }
}
