using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Musichord.Models;
using Microsoft.AspNetCore.Authorization;
using Musichord.Services;
using Musichord.Models.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Storage;

namespace Musichord.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserRepository _userRepo;
    private readonly IFriendshipRepo _friendRepo;
    public HomeController(ILogger<HomeController> logger, IUserRepository userRepo, IFriendshipRepo friendRepo)
    {
        _logger = logger;
        _userRepo = userRepo;
        _friendRepo = friendRepo;
    }

 // may have to remove with the friends stuff
    public async Task<IActionResult> Index()
    {    
        
        List<Friendship> friendsList = new();
        var users = await _userRepo.ReadAllAsync();
        var relationships = await _friendRepo.GetAllFriendshipsAsync();
        if (User.Identity != null)
        {
            
            
            var user = await _userRepo.ReadByUsernameAsync(User.Identity!.Name!);
            
            if (user != null)
            {
                ViewData["LoggedIn"] = true;
            } else
            {
                ViewData["LoggedIn"] = false;
            }

        }
        return View(friendsList);
    }

    public async Task<IActionResult> Explore()
    {
        // List<ApplicationUser?> friendsList = new();
        // if (User.Identity != null)
        // {
        //     var user = await _userRepo.ReadByUsernameAsync(User.Identity!.Name!) ?? new ApplicationUser();
                  
        //     if (user != null)
        //     {
        //         friendsList = await _friendRepo.GetAllNonFriends(user);
        //     }
        // }

        return View();
    }

    public async Task<IActionResult> Friends()
    {
        List<string> friendsList = new();
        if (User.Identity != null)
        {
            var user = await _userRepo.ReadByUsernameAsync(User.Identity.Name!);
            
            if (user != null)
            {
                var friends = await _friendRepo.GetAllFriendshipsAsync();
                var friendsList1 = friends.Where(f => f.SenderHandle == user.Handle && f.Status == "Accepted").Select(f => f.ReceiverHandle).ToList();      
                var friendsList2 = friends.Where(f => f.ReceiverHandle == user.Handle && f.Status == "Accepted").Select(f => f.SenderHandle).ToList();    
                friendsList = friendsList1.Concat(friendsList2).ToList();
            }
        }
        return View(friendsList);
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
