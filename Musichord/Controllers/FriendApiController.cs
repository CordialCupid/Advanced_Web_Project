using Microsoft.AspNetCore.Mvc;
using Musichord.Models.Entities;
using Musichord.Services;

namespace Musichord.Controllers;

[Route("api/friend")]
[ApiController]
public class FriendApiController : ControllerBase
{
    private readonly IFriendshipRepo _friendRepo;
    private readonly IUserRepository _userRepo;

    public FriendApiController(IFriendshipRepo friendRepo, IUserRepository userRepo)
    {
        _friendRepo = friendRepo;
        _userRepo = userRepo;
    }

    [HttpPost("addfriend/{username}")]
    public async Task<IActionResult> Post(string username)
    {
        var userRequested = await _userRepo.ReadByUsernameAsync(username);
        var currentUser = await _userRepo.ReadByUsernameAsync(User.Identity!.Name!);

        if (userRequested != null && currentUser != null)
        {
            GlobalFriendGraph.AddEdge(currentUser.Id, userRequested.Id );
            var newShip = await _friendRepo.CreateFriendship(currentUser, userRequested);
            return CreatedAtAction("Get", new {Id = newShip.Id}, newShip);          
        }
        throw new ArgumentException("Users being friended must both not be null");
    }
}