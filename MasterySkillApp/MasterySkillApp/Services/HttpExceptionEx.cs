using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MasterySkillApp.Services
{
    public class HttpExceptionEx : Exception
    {

        public bool Success{ get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string MessagePersonalized { get; set; }

        // Constructor 1
        public HttpExceptionEx()
        {
            Success = true;
            StatusCode = HttpStatusCode.OK;
            //Message = "Proceso exitoso";
        }

        // Constructor 2
        public HttpExceptionEx(HttpStatusCode statusCode, string messagePersonalized, bool success)
        {
            StatusCode = statusCode;
            MessagePersonalized = messagePersonalized;
            Success = success;
        }
    }
}
