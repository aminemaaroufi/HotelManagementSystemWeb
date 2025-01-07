using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HotelManagement.Models
{

    [Table("reservation_table")]
    public class Reservation
    {
        [Key]
        public int Reservation_ID { get; set; }
        public string Reservation_Room_Type { get; set; }
        public int Reservation_Room_Number { get; set; }

        [ForeignKey("Client")]
        public int Reservation_Client_ID { get; set; }
        public string Reservation_In { get; set; }
        public string Reservation_Out { get; set; }

        // Navigation property
        public virtual Client Client { get; set; }
    }


}

