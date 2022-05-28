using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Dtos
{
    public class ServiceResponse<T>
    {
        public string Message { get; set; }
        public int Code { get; set; }
        public T Data {get; set; }
        public bool Status { get; set; }
        public Dictionary<string, object> Meta { get; set; } = new Dictionary<string, object>();

        public void SetSuccess(T data, string message = "")
        {
            this.Data = data;
            Status = true;
            Code = 200;
            Message = message;
        }
        public void SetFailed(string message = "")
        {
            Status = false;
            Code = 400;
            Message = message;
        }
        public void SetPaginationData(IPagedAndSortedResultDto metaData)
        {
            this.Meta.Add("Total", metaData.TotalRecords);
            this.Meta.Add("PerPage", metaData.PerPage);
            this.Meta.Add("CurrentPage", metaData.CurrentPage);
        }
    }
}
