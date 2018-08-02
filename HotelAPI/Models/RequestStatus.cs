using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAPI.Models
{
    public enum StatusEnum
    {
        Failed,
        Success
    }
    public class RequestStatus
    {
        public StatusEnum Status { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}