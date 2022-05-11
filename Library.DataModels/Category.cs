using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    public class Category : AuditedEntity<long>
    {
        public string Name { get; set; }
        public bool Visible { get; set; }
    }
}
