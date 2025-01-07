using System;
namespace HotelManagement.Models
{
	public class BookViewModel
	{
		
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public double RoomPrice { get; set; }
        public string RoomPhone { get; set; }
        public string RoomFree { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientAddress { get; set; }
        public string ClientEmail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    
    }
}

