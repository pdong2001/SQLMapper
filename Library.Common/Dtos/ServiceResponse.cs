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
        public void SetSuccess(T data, string message = "")
        {
            this.Data = data;
            Status = true;
            Code = 200;
            Message = message;
        }
    }
}
