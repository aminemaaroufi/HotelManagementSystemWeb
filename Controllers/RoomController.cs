using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Models;

namespace HotelManagement.Controllers;

public class RoomController : Controller
{
    private readonly HotelDbContext _context;

    public RoomController(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> AvailableRooms()
    {
        // Fetch all rooms from the database
        var rooms = await _context.Rooms.ToListAsync();
        return View(rooms);
    }

    // GET method for booking
    [HttpGet]
    [HttpGet]
    public async Task<IActionResult> Book(int roomNumber)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Room_Number == roomNumber);
        if (room == null || room.Room_Free.ToLower() != "yes")
        {
            TempData["ErrorMessage"] = "Room not available for booking.";
            return RedirectToAction("AvailableRooms");
        }

        // Create the view model to pass to the view
        var viewModel = new BookViewModel
        {
            RoomNumber = room.Room_Number,
            RoomType = room.Room_Type,
            RoomPrice = (double)room.Room_Price,
            RoomPhone = room.Room_Phone,
            RoomFree = room.Room_Free
        };

        return View(viewModel);
    }


    // POST method to process the booking
    [HttpPost]
    public async Task<IActionResult> Book(BookViewModel model)
    {
        // Fetch the client based on email or phone number
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Client_email == model.ClientEmail);

        // If client does not exist, create a new client
        if (client == null)
        {
            client = new Client
            {
                Client_FirstName = model.ClientFirstName,
                Client_LastName = model.ClientLastName,
                Client_Phone = model.ClientPhone,
                //Client_Address = model.ClientAddress,
                Client_email = model.ClientEmail
            };
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        // Check if the room is still available
        var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Room_Number == model.RoomNumber);
        if (room == null || room.Room_Free.ToLower() != "yes")
        {
            TempData["ErrorMessage"] = "Room not available for booking.";
            return RedirectToAction("AvailableRooms");
        }

        // Create a new reservation
        var reservation = new Reservation
        {
            Reservation_Room_Number = model.RoomNumber,
            Reservation_Room_Type = room.Room_Type,
            Reservation_Client_ID = client.Client_ID,
            Reservation_In = model.StartDate.ToString("yyyy-MM-dd"),
            Reservation_Out = model.EndDate.ToString("yyyy-MM-dd"),

        };

        // Save the reservation to the database
        _context.Reservations.Add(reservation);

        room.Room_Free = "no"; // Mark the room as booked
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Your reservation request has been received! Please check your email for confirmation details.";
        return RedirectToAction("BookingSuccess", new { id = reservation.Reservation_ID });
    }

    public async Task<IActionResult> BookingSuccess(int id)
    {
        var reservation = await _context.Reservations
        .Include(r => r.Client)
        .FirstOrDefaultAsync(r => r.Reservation_ID == id);

        if (reservation == null)
        {
            return NotFound();
        }

        var room = await _context.Rooms
            .FirstOrDefaultAsync(r => r.Room_Number == reservation.Reservation_Room_Number);

        var viewModel = new BookingSuccessViewModel
        {
            Reservation = reservation,
            Room = room 
        };

        return View(viewModel);
    }
}
