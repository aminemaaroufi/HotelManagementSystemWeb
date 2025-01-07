using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Models
{
	public class HotelDbContext : DbContext
	{
		public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

		public DbSet <Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Client)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.Reservation_Client_ID);
        }


    }
}

