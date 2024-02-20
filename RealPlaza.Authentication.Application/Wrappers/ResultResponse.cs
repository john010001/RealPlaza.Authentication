using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Application.Wrappers
{
    public class ResultResponse<T>
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public ResultResponse()
        {
            Code = HttpStatusCode.OK;
        }

        public ResultResponse(HttpStatusCode httpStatusCode)
        {
            Code = httpStatusCode;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public ResultResponse(HttpStatusCode httpStatusCode, T data, string message)
        {
            Code = httpStatusCode;
            Data = data;
            Message = message;
        }
    }
}
