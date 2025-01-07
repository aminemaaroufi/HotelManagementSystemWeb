using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HotelManagement.Models
{
    [Table("client_table")]
    public class Client
    {
        [Key]
        public int Client_ID { get; set; }
        public string Client_FirstName { get; set; }
        public string Client_LastName { get; set; }
        public string Client_Phone { get; set; }
        public string Client_email { get; set; }

        // Virtual collection for the one-to-many relationship
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}

