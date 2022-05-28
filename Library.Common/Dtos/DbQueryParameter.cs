using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Dtos
{
    /// <summary>
    /// Không chỉnh sửa phần này!!!
    /// </summary>
    public class DbQueryParameter
    {
        public DbQueryParameter() { }
        public DbQueryParameter(string name, object value, CompareOperator compareOperator = CompareOperator.Like, LogicOperator logicOperator = LogicOperator.AND) {
            this.Name = name;
            this.Value = value;
            this.CompareOperator = compareOperator;
            this.LogicOperator = logicOperator;
        }
        public string Name { get; set; }
        public object Value { get; set; }
        public CompareOperator CompareOperator { get; set; } = CompareOperator.Like;
        public LogicOperator LogicOperator { get; set; }
        public string GetCompareOperator()
        {
            switch (CompareOperator)
            {
                case CompareOperator.Equal: return "=";
                case CompareOperator.NotSame: return "!=";
                case CompareOperator.LessThan: return "<";
                case CompareOperator.GreaterThan: return ">";
                case CompareOperator.GreaterThanOrEqual: return ">=";
                case CompareOperator.LessThanOrEqual: return "<=";
                case CompareOperator.Like: return "LIKE";
            }
            return "";
        }

        public string GetLogicOperator()
        {
            return LogicOperator.ToString();
        }
    }

    public enum CompareOperator
    {
        Equal,
        NotSame,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Like
    }

    public enum LogicOperator
    {
        OR,
        AND
    }
}
