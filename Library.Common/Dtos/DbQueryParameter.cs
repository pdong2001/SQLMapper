using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Dtos
{
    public class DbQueryParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public CompareOperator CompareOperator { get; set; }
        public LogicOperator LogicOperator { get; set; }
        public string GetCompareOperator()
        {
            switch (CompareOperator)
            {
                case CompareOperator.Same: return "=";
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
        Same,
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
