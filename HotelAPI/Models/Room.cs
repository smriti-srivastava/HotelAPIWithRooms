using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAPI.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public RoomTypeList RoomType { get; set; }
        public int NumberOfRoomsBooked { get; set; }
        public int NumberOfRoomsPresent { get; set; }

    }


    public enum RoomTypeList
    {
        Single,
        Double,
        Deluxe,
        TwinRoom,
        Studio,
        Suite
    }

}