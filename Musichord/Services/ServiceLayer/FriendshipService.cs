using Musichord.Services.Interfaces.Friends;
using Musichord.Models.Entities;

namespace Musichord.Services.ServiceLayer;


// inherited interface usable for friendship service class
public class FriendshipService : IFriendshipService 
{
    private readonly IFriendshipRepo _friendRepo;

    public FriendshipService(IFriendshipRepo friendRepo)
    {
        _friendRepo = friendRepo;
    }

    public async Task<Friendship?> ReadAsync(string sender, string receiver)
    {
        return await _friendRepo.ReadAsync(sender, receiver);
    }

    public async Task<ICollection<string>> GetAllNonFriends(ICollection<string> exceptUser, ApplicationUser user)
    {
        ICollection<string> friendsList = new List<string>();

        if (user != null)
        {
            var friendships = await GetAllFriendsHandlesAsync(user.Handle);
            friendsList = exceptUser.Except(friendships).ToList();   
        }
        return friendsList;
    }

    public async Task<ICollection<Friendship>> GetAllFriendshipsAsync()
    {
        return await _friendRepo.GetAllFriendshipsAsync();
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

        await _friendRepo.CreateFriendship(FriendshipToAdd);
        return FriendshipToAdd; 
    }

    public async Task DeleteFriendship(string sender, string receiver)
    {
        if (sender != null && receiver != null)
        {
            Friendship? toDelete = await ReadAsync(sender, receiver);
            if (toDelete != null)
            { 
                await _friendRepo.DeleteFriendship(toDelete);
            }
        }
    }
    
    public async Task UpdateFriendshipStatus(string senderHandle, string receiverHandle)
    {
        Friendship? toUpdate = await ReadAsync(senderHandle, receiverHandle);
        if (toUpdate != null)
        {
            await _friendRepo.UpdateFriendshipStatus(toUpdate);
        }
    }
}