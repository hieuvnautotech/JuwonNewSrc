using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; }
        public int HttpResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public T Data { get; set; }

        public ResponseModel()
        {
            HttpResponseCode = 200;
            IsSuccess = false;
            ResponseMessage = Resource.ERROR_SystemError;
            Data = default;
        }
    }
}
