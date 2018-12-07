using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MasterySkillApp.Models
{
    public class BaseResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string message { get; set; }
        public bool IsSuccessStatusCode { get; set; }

    }
}
