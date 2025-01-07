using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
    [Table("room_table")]
    public class Room
    {
        [Key]
        public int Room_Number { get; set; }
        public string Room_Type { get; set; }
        public string Room_Phone { get; set; }
        public string Room_Free { get; set; }
        public double? Room_Price { get; set; }
    }
}