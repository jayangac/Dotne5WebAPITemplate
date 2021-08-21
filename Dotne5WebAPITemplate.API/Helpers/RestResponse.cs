using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Dotne5WebAPITemplate.API.Helpers
{
    public class RestResponse
    {
        public HttpStatusCode _statusCode;
        public bool _isSuccsess;
        public string _message;
        public object _data;

        public enum Message { Success, Error }

        public RestResponse(HttpStatusCode status, bool isSuccess, string message, object data)
        {
            _statusCode = status;
            _isSuccsess = isSuccess;
            _message = message;
            _data = data;
        }
    }
}
