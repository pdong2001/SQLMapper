using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
    public class TableNameAttribute : System.Attribute
    {
        public string TableName { get;private set; }
        public TableNameAttribute(string TableName)
        {
            this.TableName = TableName;
        }
    }
}
