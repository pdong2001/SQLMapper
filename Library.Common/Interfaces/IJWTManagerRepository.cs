using Library.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Interfaces
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(string email, string password);
    }
}
