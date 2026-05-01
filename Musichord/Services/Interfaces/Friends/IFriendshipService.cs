using Musichord.Services.Interfaces;
using Musichord.Models.Entities;

namespace Musichord.Services.Interfaces.Friends;


// inherited interface usable for friendship service class
public interface IFriendshipService 
{
    Task<Friendship?> ReadAsync(string sender, string receiver);
    Task<ICollection<string>> GetAllNonFriends(ICollection<string> exceptUser, ApplicationUser user);
    Task<ICollection<Friendship>> GetAllFriendshipsAsync();
    Task<ICollection<string>> GetAllFriendsHandlesAsync(string handle);
    Task<Friendship> CreateFriendship(ApplicationUser sender, ApplicationUser receiver);
    Task DeleteFriendship(string sender, string receiver);
    Task UpdateFriendshipStatus(string senderHandle, string receiverHandle);
}