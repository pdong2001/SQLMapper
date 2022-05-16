using Library.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Interfaces
{
    public interface IDatabaseHelper : IDisposable
    {
        void CreateTablesIfNotExsist();
        void AddTableConstraints();
        void DropAllTable();
        EntitySet<User> Users { get; set; }
    }
}
