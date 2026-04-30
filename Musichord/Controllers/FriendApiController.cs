using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Musichord.Models.Entities;
using Musichord.Services;

namespace Musichord.Controllers;

[Route("api/friend")]
[ApiController]
[Authorize]
public class FriendApiController : ControllerBase
{
    private readonly IFriendshipRepo _friendRepo;
    private readonly IUserRepository _userRepo;
    private readonly ITrackRepository _trackRepo;

    public FriendApiController(IFriendshipRepo friendRepo, IUserRepository userRepo, ITrackRepository trackRepo)
    {
        _friendRepo = friendRepo;
        _userRepo = userRepo;
        _trackRepo = trackRepo;
    }

    [HttpGet("nonfriends")]
    public async Task<IActionResult> Get()
    {
        Console.WriteLine("==================================================================================================================");
        Console.WriteLine("==================================================================================================================");
        Console.WriteLine("==================================================================================================================");
        Console.WriteLine("==================================================================================================================");
        Console.WriteLine("==================================================================================================================");
        Console.WriteLine("==================================================================================================================");
        Console.WriteLine("==================================================================================================================");

        Console.WriteLine(User.Identity.Name);
        var user = await _userRepo.ReadByUsernameAsync(User.Identity.Name) ?? new ApplicationUser();
        var users = await _friendRepo.GetAllNonFriends(user);
        Console.WriteLine("==================================================================================================================");
        Console.WriteLine("==================================================================================================================");
        Console.WriteLine("==================================================================================================================");
        Console.WriteLine("==================================================================================================================");
        Console.WriteLine("==================================================================================================================");
        Console.WriteLine("==================================================================================================================");
        Console.WriteLine("==================================================================================================================");

        Console.WriteLine(users.Count());
        return Ok(users);
    }

    [HttpGet("nonfriends/records")]
    [ActionName("Get")]
    public async Task<IActionResult> GetRecords()
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }
        var user = await _userRepo.ReadByUsernameAsync(User.Identity?.Name);
        return Ok(await _userRepo.ReadAllExceptAsync(user.Handle));
    }


    [HttpPost("addfriend/{username}")]
    public async Task<IActionResult> Post(string username)
    {
        // find a way to check if there is a pending request already in place, if so accept that one... wait this is a put don't do that here
        var userRequested = await _userRepo.ReadByHandleAsync(username);
        var currentUser = await _userRepo.ReadByUsernameAsync(User.Identity!.Name!);

        if (userRequested != null && currentUser != null)
        {
            var newShip = await _friendRepo.CreateFriendship(currentUser, userRequested);
            return CreatedAtAction("Get", new {Id = newShip.Id}, newShip);          
        }
        throw new ArgumentException("Users being friended must both not be null");
    }
    

    // okay this is what triggers when you accept a friend request, it should find the pending request and update the status to accepted
    [HttpPut("updatestatus/{username}")]
    public async Task<IActionResult> Put(string username)
    {
        var currentUser = await _userRepo.ReadByUsernameAsync(User.Identity!.Name!);
        if (username != null && currentUser != null)
        {
            var allShips = await _friendRepo.GetAllFriendshipsAsync();
            var pendingShip = allShips.FirstOrDefault(s => s.SenderHandle == username && s.ReceiverHandle == currentUser.Handle && s.Status == "Pending");
            if (pendingShip != null)
            {
                pendingShip.Status = "Accepted";
                await _friendRepo.UpdateFriendshipStatus(pendingShip.SenderHandle, pendingShip.ReceiverHandle);
                return Ok(new { success = true });          
            }
            return NoContent();          
        }
        throw new ArgumentException("Users being friended must both not be null");
    }

    // triggers when declining a friend request, should find the pending request and delete it, or delete an already accepted friendship if you unfriend someone
    [HttpDelete("removefriend/{username}")]
    public async Task<IActionResult> Delete(string username)
    {
        var currentUser = await _userRepo.ReadByUsernameAsync(User.Identity!.Name!);
        if (username != null && currentUser != null)
        {
            var allShips = await _friendRepo.GetAllFriendshipsAsync();
            var deleteShip = allShips.FirstOrDefault(s => (s.SenderHandle == username && s.ReceiverHandle == currentUser.Handle) || (s.ReceiverHandle == username && s.SenderHandle == currentUser.Handle));
            if (deleteShip != null)
            {
                await _friendRepo.DeleteFriendship(deleteShip.SenderHandle, deleteShip.ReceiverHandle);
                return NoContent();          
            }
            return NoContent();          
        }
        return NoContent();
    }
}