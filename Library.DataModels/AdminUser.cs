using Library.Common.Attributes;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    [TableName("AdminUsers")]
    public class AdminUser : AuditedEntity<long>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}