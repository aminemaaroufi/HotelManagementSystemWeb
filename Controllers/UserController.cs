using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Models;

namespace HotelManagement.Controllers;

public class UserController : Controller
{
    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }
}

