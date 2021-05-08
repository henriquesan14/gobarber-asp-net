using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiGoBarber.ExceptionUtil
{
    public class HttpResponseException : Exception
    {

        public HttpResponseException(HttpStatusCode status, object value)
        {
            Status = status;
            Value = value;
        }
        public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;

        public object Value { get; set; }
    }
}
