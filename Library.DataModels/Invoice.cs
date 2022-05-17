using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    public class Invoice : AuditedEntity<long>
    {
        public string Customer_Name { get; set; }

        public string Customer_Email { get; set; }

        public string Customer_Phone { get; set; }

        public string Address { get; set; }

        public string Province { get; set; }

        public string District { get; set; }

        public string Commune { get; set; }

        public long Total { get; set; }
    }
}
