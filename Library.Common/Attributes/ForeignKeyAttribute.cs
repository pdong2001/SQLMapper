using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ForeignKeyAttribute : Attribute
    {
        private Type table;
        public string GetTableName()
        {
            var tableNameAttribute = (TableNameAttribute)Attribute.GetCustomAttribute(table, typeof(TableNameAttribute));
            if (tableNameAttribute == null)
            {
                if (table.Name.EndsWith('y'))
                {
                    return table.Name.Substring(0, table.Name.Length - 1) + "es";
                }
                else
                {
                    return table.Name + "s";
                }
            }
            else
            {
                return tableNameAttribute.TableName;
            }
        }
        public ForeignKeyAttribute(Type table)
        {
            this.table = table;
        }
    }
}
