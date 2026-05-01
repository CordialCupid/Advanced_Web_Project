using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Musichord.Models.Entities;
using Musichord.Services;
using Musichord.Services.Interfaces.Friends;
using Musichord.Services.Interfaces.TrackInterfaces;

namespace Musichord.Controllers;

[Route("api/friend")]
[ApiController]
[Authorize]
public class FriendApiController : ControllerBase
{
    private readonly IFriendshipService _friendService;
    private readonly IUserRepository _userRepo;
    private readonly ITrackRepository _trackRepo;

    public FriendApiController(IFriendshipService friendService, IUserRepository userRepo, ITrackRepository trackRepo)
    {
        _friendService = friendService;
        _userRepo = userRepo;
        _trackRepo = trackRepo;
    }

    [HttpGet("nonfriends")]
    public async Task<IActionResult> Get()
    {
        if (User.Identity?.Name != null)
        {
            var user = await _userRepo.ReadByUsernameAsync(User.Identity.Name) ?? new ApplicationUser();
            var exceptUser = await _userRepo.ReadAllHandlesExceptUserAsync(user.Handle);
            var users = await _friendService.GetAllNonFriends(exceptUser, user);

            List<ApplicationUser> nons = new();
            foreach (var u in users)
            {
                var non = await _userRepo.ReadByHandleAsync(u);
                if (non != null)
                {
                    nons.Add(non);             
                }
            }
            return Ok(nons);         
        }
        return Unauthorized();
    }

    [HttpGet("nonfriends/records")]
    [ActionName("Get")]
    public async Task<IActionResult> GetRecords()
    {
        if (User.Identity?.Name != null)
        {
            var user = await _userRepo.ReadByUsernameAsync(User.Identity.Name) ?? new ApplicationUser();
            var records = await _trackRepo.GetAllRecordExceptByUser(user.Handle);
            return Ok(records);
        }
        return Unauthorized();
    }


    [HttpPost("addfriend/{username}")]
    public async Task<IActionResult> Post(string username)
    {
        // find a way to check if there is a pending request already in place, if so accept that one... wait this is a put don't do that here
        var userRequested = await _userRepo.ReadByHandleAsync(username);
        var currentUser = await _userRepo.ReadByUsernameAsync(User.Identity?.Name!);

        if (userRequested != null && currentUser != null)
        {
            var newShip = await _friendService.CreateFriendship(currentUser, userRequested);
            return CreatedAtAction("Get", new {Id = newShip.Id}, newShip);          
        }
        return Unauthorized();
    }
    

    // okay this is what triggers when you accept a friend request, it should find the pending request and update the status to accepted
    [HttpPut("updatestatus/{username}")]
    public async Task<IActionResult> Put(string username)
    {
        var currentUser = await _userRepo.ReadByUsernameAsync(User.Identity!.Name!);
        if (username != null && currentUser != null)
        {
            await _friendService.UpdateFriendshipStatus(username, currentUser.Handle);
            return Ok(new { success = true });                   
        }
        return Unauthorized();
    }

    // triggers when declining a friend request, should find the pending request and delete it, or delete an already accepted friendship if you unfriend someone
    [HttpDelete("removefriend/{username}")]
    public async Task<IActionResult> Delete(string username)
    {
        var currentUser = await _userRepo.ReadByUsernameAsync(User.Identity!.Name!);
        if (username != null && currentUser != null)
        {
            var allShips = await _friendService.GetAllFriendshipsAsync();
            var deleteShip = allShips.FirstOrDefault(s => (s.SenderHandle == username && s.ReceiverHandle == currentUser.Handle) || (s.ReceiverHandle == username && s.SenderHandle == currentUser.Handle));
            if (deleteShip != null)
            {
                await _friendService.DeleteFriendship(deleteShip.SenderHandle, deleteShip.ReceiverHandle);
                return NoContent();          
            }
            return NoContent();          
        }
        return NoContent();
    }
}