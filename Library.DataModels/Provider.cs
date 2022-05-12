﻿using Library.Common.Attributes;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    [TableName("Providers")]
    public class Provider : AuditedEntity<long>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool Visible { get; set; }
    }
}
