using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Musichord.Models;
using Microsoft.AspNetCore.Authorization;
using Musichord.Services;
using Musichord.Models.Entities;

namespace Musichord.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;
    private readonly IUserRepository _userRepo;

    public ProfileController(ILogger<ProfileController> logger, IUserRepository userRepo)
    {
        _logger = logger;
        _userRepo = userRepo;
    }

    // should return the logged in user's profile
    public async Task<IActionResult> Index()
    {
        ApplicationUser? user = await _userRepo.ReadByUsernameWithTracksAsync(User.Identity?.Name);

        var tracks = user?.FavoriteTracks.Select(ft => ft.Track)?.ToList();

        ViewData["Handle"] = user?.Handle;
        ViewData["ProfilePicture"] = user?.ProfilePicture;
        ViewData["ProfileOwner"] = user;
        ViewData["IsOwnProfile"] = true;
        return View(tracks);
    }

    // should return a specific user's profile, checking for authorization
    [Route("Profile/{handle}")]
    public async Task<IActionResult> Details(string handle)
    {
        ApplicationUser? user = await _userRepo.ReadByHandleAsync(handle);
        if (user == null)
        {
            return NotFound();
        }
        
        var tracks = user.FavoriteTracks.Select(ft => ft.Track).ToList();
        ViewData["Handle"] = user.Handle;
        ViewData["ProfilePicture"] = user.ProfilePicture;
        ViewData["ProfileOwner"] = user;
        ViewData["IsOwnProfile"] = User.Identity?.IsAuthenticated == true && 
                                User.Identity.Name == user.UserName;
        return View("Index", tracks);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


