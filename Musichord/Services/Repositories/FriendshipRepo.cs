using Humanizer;
using Microsoft.EntityFrameworkCore;
using Musichord.Services;
using Microsoft.AspNetCore.Authorization;

namespace Musichord.Models.Entities;

public class FriendshipRepo : IFriendshipRepo
{
    private readonly ApplicationDbContext _db;
    private readonly IUserRepository _userRepo;

    public FriendshipRepo(ApplicationDbContext db, IUserRepository userRepo)
    {
        _db = db;
        _userRepo = userRepo;
    }

    public async Task<Friendship?> ReadAsync(string sender, string receiver)
    {
        return await _db.Friendships
                        .Include(f => f.Sender)
                        .Include(f => f.Receiver)
                        .FirstOrDefaultAsync(f => f.SenderHandle == sender && f.ReceiverHandle == receiver);
    }

    public async Task<List<ApplicationUser?>> GetAllNonFriends(ApplicationUser user)
    {
        List<ApplicationUser?> friendsList = new List<ApplicationUser?>();
        if (user != null)
        {
            var exceptUser = await _userRepo.ReadAllExceptAsync(user.Email);
            var friends = await GetAllFriendsAsync(user.Handle);
            friendsList = exceptUser.Except(friends).ToList();
        }
        return friendsList;
    }

    public async Task<ICollection<Friendship>> GetAllFriendshipsAsync()
    {
        return await _db.Friendships
                        .Include(f => f.Sender)
                        .Include(f => f.Receiver)
                        .ToListAsync();
    }

    public async Task<ICollection<string>> GetAllFriendsHandlesAsync(string handle)
    {
        var relationships = await GetAllFriendshipsAsync();
        var usersRelationships = relationships.Where(f => f.ReceiverHandle == handle || f.SenderHandle == handle).ToList();
        var userSent = usersRelationships.Where(f => f.SenderHandle == handle).Select(f => f.ReceiverHandle).ToList();
        var userReceived = usersRelationships.Where(f => f.ReceiverHandle == handle).Select(f => f.SenderHandle).ToList();

        var friendsHandles = userSent.Concat(userReceived).ToList();

        return friendsHandles;
    }

    public async Task<ICollection<ApplicationUser?>> GetAllFriendsAsync(string handle)
    {
        var relationships = await GetAllFriendshipsAsync();
        var usersRelationships = relationships.Where(f => f.ReceiverHandle == handle || f.SenderHandle == handle).ToList();
        var userSent = usersRelationships.Where(f => f.SenderHandle == handle).Select(f => f.ReceiverHandle).ToList();
        var userReceived = usersRelationships.Where(f => f.ReceiverHandle == handle).Select(f => f.SenderHandle).ToList();

        var friendsHandles = userSent.Concat(userReceived).ToList();
        List<ApplicationUser?> friends = new();

        foreach(var friend in friendsHandles)
        {
            friends.Add(await _userRepo.ReadByHandleAsync(friend));
        }

        return friends;
    }

    public async Task<Friendship> CreateFriendship(ApplicationUser sender, ApplicationUser receiver)
    {
        Friendship FriendshipToAdd = new Friendship();

        FriendshipToAdd.Sender = sender;
        FriendshipToAdd.SenderHandle = sender.Handle;
        FriendshipToAdd.SenderId = sender.Id;
        FriendshipToAdd.Receiver = receiver;
        FriendshipToAdd.ReceiverHandle = receiver.Handle;
        FriendshipToAdd.ReceiverId = receiver.Id;
        FriendshipToAdd.Status = "Pending";

        await _db.Friendships.AddAsync(FriendshipToAdd);
        await _db.SaveChangesAsync();
        return FriendshipToAdd; 
    }

    public async Task DeleteFriendship(string sender, string receiver)
    {
        if (sender != null && receiver != null)
        {
            Friendship? toDelete = await ReadAsync(sender, receiver);
            if (toDelete != null)
            { 
                _db.Friendships.Remove(toDelete);
                await _db.SaveChangesAsync();
            }
        }
    }

    public async Task UpdateFriendshipStatus(string currentHandle, string senderHandle)
    {
        if (currentHandle != null && senderHandle != null)
        {
            Friendship? toUpdate = await ReadAsync(senderHandle, currentHandle);
            if (toUpdate != null)
            {
                toUpdate.Status = "Accepted";
                await _db.SaveChangesAsync();
            }
        }
    }
}