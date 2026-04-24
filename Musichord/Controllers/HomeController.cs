using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Musichord.Models;
using Microsoft.AspNetCore.Authorization;
using Musichord.Services;
using Musichord.Models.Entities;

namespace Musichord.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserRepository _userRepo;

    public HomeController(ILogger<HomeController> logger, IUserRepository userRepo)
    {
        _logger = logger;
        _userRepo = userRepo;
    }

    public async Task<IActionResult> Index()
    {
        ApplicationUser? user = await _userRepo.ReadByUsernameAsync(User.Identity!.Name!);
        var tracks = user?.FavoriteTracks.Select(ft => ft.Track).ToList();
        return View(tracks);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
