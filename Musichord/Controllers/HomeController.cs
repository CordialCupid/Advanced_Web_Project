using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Musichord.Models;
using Microsoft.AspNetCore.Authorization;
using Musichord.Services;
using Musichord.Models.Entities;
using Microsoft.AspNetCore.Components;

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

    public async Task<IActionResult> Index()
    {
        var users = await _userRepo.ReadAllAsync();
        var relationships = await _friendRepo.GetAllFriendshipsAsync();
        var user = await _userRepo.ReadByUsernameAsync(User.Identity!.Name!);
        var exceptUser = await _userRepo.ReadAllExceptAsync(User.Identity!.Name!);
        
        await GlobalFriendGraph.GraphInit(users, relationships);
        var nons = await GlobalFriendGraph.GetNonFriends(user!.Id, exceptUser);
        return View(nons);
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
