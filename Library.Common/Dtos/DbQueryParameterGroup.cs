using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Dtos
{
    public class DbQueryParameterGroup
    {
        public DbQueryParameterGroup(params DbQueryParameter[] dbQueryParameter)
        {
            this.dbQueryParameters = dbQueryParameter.ToList();
            this.LogicOperator = LogicOperator.AND;
        }
        public DbQueryParameterGroup(LogicOperator logicOperator = LogicOperator.AND, params DbQueryParameter[] dbQueryParameter)
        {
            this.dbQueryParameters = dbQueryParameter.ToList();
            this.LogicOperator = logicOperator;
        }
        public IList<DbQueryParameter> dbQueryParameters { get; set; }
        public LogicOperator LogicOperator { get; set; }
    }
}
