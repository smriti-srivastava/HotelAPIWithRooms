using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAPI.Models
{
    public class ApiResponse
    {
        public List<Hotel> Hotels { get; set; }
         
        public RequestStatus Status { get; set; }
       

    }
    
}