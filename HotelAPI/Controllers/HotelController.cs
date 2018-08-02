using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HotelAPI.Models;

namespace HotelAPI.Controllers
{
    
    public class HotelController : ApiController
    {
        public static List<Hotel> _hotels = new List<Hotel>()
        {
            new Hotel()
            {
                HotelId =1,
                Name ="Novotel",
            Location="Pune",
                Rooms = new List<Room>()
                {
                    new Room(){ RoomId = 1, RoomType = RoomTypeList.Single, NumberOfRoomsBooked=0, NumberOfRoomsPresent=20 },
                    new Room(){ RoomId = 2, RoomType = RoomTypeList.Double, NumberOfRoomsBooked=0, NumberOfRoomsPresent=30 },
                    new Room(){ RoomId = 3, RoomType = RoomTypeList.Deluxe, NumberOfRoomsBooked=0, NumberOfRoomsPresent=25 },
                    new Room(){ RoomId = 4, RoomType = RoomTypeList.TwinRoom, NumberOfRoomsBooked=0, NumberOfRoomsPresent=35 },
                }

            },
             new Hotel()
            {
                HotelId =2,
                Name ="Hyatt Recedency",
                Location="Pune",
                Rooms = new List<Room>()
                {
                    new Room(){ RoomId = 1, RoomType = RoomTypeList.Single, NumberOfRoomsBooked=0, NumberOfRoomsPresent=20 },
                    new Room(){ RoomId = 2, RoomType = RoomTypeList.Double, NumberOfRoomsBooked=0, NumberOfRoomsPresent=30 },
                    new Room(){ RoomId = 3, RoomType = RoomTypeList.Suite, NumberOfRoomsBooked=0, NumberOfRoomsPresent=25 },
                    new Room(){ RoomId = 4, RoomType = RoomTypeList.Studio, NumberOfRoomsBooked=0, NumberOfRoomsPresent=35 },
                }

            }

        };

        [HttpGet]
        public ApiResponse GetAllHotels()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
               
                if(_hotels != null)
                {
                    apiResponse.Hotels = _hotels;
                    apiResponse.Status = new RequestStatus()
                    {
                        Status = StatusEnum.Success,
                        StatusCode = 200,
                        StatusMessage = "List Of Hotels Successfully Sent"
                    };  
                }
                else
                {
                    apiResponse.Hotels = null;
                    apiResponse.Status = new RequestStatus()
                    {
                        Status = StatusEnum.Failed,
                        StatusCode = 204,
                        StatusMessage = "No Hotel Found"
                    };
                }
                
            }
            catch
            {
              apiResponse = ExceptionResponse();
            }
            return apiResponse;
        }
        [HttpGet]
        public ApiResponse GetHotelById(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                Hotel requestedhotel = _hotels.Find(hotel => hotel.HotelId == id);
                if (requestedhotel != null)
                {
                    apiResponse.Hotels = new List<Hotel>() { requestedhotel };
                    apiResponse.Status = new RequestStatus()
                    {
                        Status = StatusEnum.Success,
                        StatusCode = 200,
                        StatusMessage = "Hotel With " + id + " ID Found!"
                    };
                }
                else
                {
                    apiResponse.Hotels = null;
                    apiResponse.Status = new RequestStatus()
                    {
                        Status = StatusEnum.Failed,
                        StatusCode = 404,
                        StatusMessage = "Hotel With " + id + " ID Not Found!"
                    };
                }
            }
            catch
            {
                apiResponse = ExceptionResponse();
            }
            return apiResponse;
        }
        
        public ApiResponse CreateHotel(Hotel hotel)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                if(hotel != null)
                {
                    _hotels.Add(hotel);
                    apiResponse.Hotels = _hotels;
                    apiResponse.Status = new RequestStatus()
                    {
                        Status = StatusEnum.Success,
                        StatusCode = 200,
                        StatusMessage = "Hotel Successfully Added!"
                    };
                }
                else
                {
                    apiResponse.Hotels = null;
                    apiResponse.Status = new RequestStatus()
                    {
                        Status = StatusEnum.Failed,
                        StatusCode = 422,
                        StatusMessage = "Invalid Data Sent"
                    };
                }
            }
            catch
            {
                apiResponse = ExceptionResponse();
            }
            return apiResponse;
        }

        [HttpDelete]
        public ApiResponse RemoveHotel(int id)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                Hotel hotelToBeDeleted = _hotels.Find(hotel => hotel.HotelId == id);
                if(hotelToBeDeleted!=null)
                {
                    _hotels.Remove(hotelToBeDeleted);
                    response.Hotels = _hotels;
                    response.Status = new RequestStatus()
                    {
                        Status = StatusEnum.Success,
                        StatusCode = 200,
                        StatusMessage = "Hotel Successfully Removed!"
                    };

                }
                else
                {
                    response.Hotels = null;
                    response.Status = new RequestStatus()
                    {
                        Status = StatusEnum.Failed,
                        StatusCode = 404,
                        StatusMessage = "Hotel With The Given Id Doesn't Exists"
                    };
                }
            }
            catch
            {
                response = ExceptionResponse();
            }
            return response;
        }

        [HttpPut]
        [Route("api/Hotel/{hotelId}/Room/{roomId}/{numberOfRoomsToBeBooked}")]
        public ApiResponse BookRoom(int hotelId, int numberOfRoomsToBeBooked, int roomId)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                Hotel hotelToBeBooked = _hotels.Find(hotel => hotel.HotelId == hotelId);
                if(hotelToBeBooked!=null)
                {
                    Room roomToBeBooked = hotelToBeBooked.Rooms.Find(room => room.RoomId == roomId);
                    if(roomToBeBooked!=null)
                    {
                        int availableRooms = roomToBeBooked.NumberOfRoomsPresent - roomToBeBooked.NumberOfRoomsBooked;
                        if (availableRooms >= numberOfRoomsToBeBooked)
                        {
                            roomToBeBooked.NumberOfRoomsBooked += numberOfRoomsToBeBooked;
                            roomToBeBooked.NumberOfRoomsPresent -= numberOfRoomsToBeBooked;
                            apiResponse.Hotels = null;
                            apiResponse.Status = new RequestStatus()
                            {
                                Status = StatusEnum.Success,
                                StatusCode = 200,
                                StatusMessage = "Room Booked Successfully"
                            };
                        }
                        else
                        {
                            apiResponse.Hotels = null;
                            apiResponse.Status = new RequestStatus()
                            {
                                Status = StatusEnum.Failed,
                                StatusCode = 500,
                                StatusMessage = "Currently This Type Of Room Is Not Present"
                            };

                        }
                    }
                    else
                    {
                        apiResponse.Hotels = null;
                        apiResponse.Status = new RequestStatus()
                        {
                            Status = StatusEnum.Failed,
                            StatusCode = 500,
                            StatusMessage = "This Type Of Room Doesn't Exists"
                        };
                    }
                }
                else
                {
                    apiResponse.Hotels = null;
                    apiResponse.Status = new RequestStatus()
                    {
                        Status = StatusEnum.Failed,
                        StatusCode = 404,
                        StatusMessage = "Hotel With The Given Id Doesn't Exists"
                    };
                }

            }
            catch
            {
                return ExceptionResponse();
            }
            return apiResponse;
        }
        ApiResponse ExceptionResponse()
        {
            return new ApiResponse()
            {
                Hotels = null,
                Status = new RequestStatus()
                {
                    Status = StatusEnum.Success,
                    StatusCode = 500,
                    StatusMessage = "Something Went Wrong!"
                }

            };
        }
    }
}
