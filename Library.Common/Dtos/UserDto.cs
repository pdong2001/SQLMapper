using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Dtos
{
    public class UserDto : AuditedEntity<long>
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
