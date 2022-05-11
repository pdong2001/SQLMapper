using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    public static class SQLDataTypeDetector
    {
        public static string GetSQLTypeName(this Type type)
        {
            string result;
            if (type.Name.Contains(nameof(Nullable)))
            {
                type = Nullable.GetUnderlyingType(type);
                result = " NULL";
            }
            else
            {
                result = " NOT NULL";
            }
            switch (type.Name)
            {
                case nameof(String): result = "NVARCHAR(MAX)" + result; break ;
                case nameof(DateTime): result =  "DATETIME2" + result; break;
                case nameof(Int32): result =  "INT" + result; break;
                case nameof(Int64): result =  "BIGINT" + result; break;
                case nameof(Boolean): result =  "BIT" + result; break;
                case nameof(Guid): result =  "UNIQUEIDENTIFIER" + result; break;
                case nameof(Double):
                case nameof(Single): result =  "FLOAT" + result; break;
                default : throw new Exception("Unknow data type");
            }
            return result;
        }
    }
}
