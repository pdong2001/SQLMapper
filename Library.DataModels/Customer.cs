using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    public class Customer : AuditedEntity<long>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
