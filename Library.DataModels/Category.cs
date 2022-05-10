using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    public class Category : AuditedEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
