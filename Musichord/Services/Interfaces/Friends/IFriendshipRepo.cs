using Musichord.Models.Entities;
namespace Musichord.Services.Interfaces.Friends;

public interface IFriendshipRepo
{
    Task<Friendship?> ReadAsync(string sender, string receiver);
    Task<Friendship> CreateFriendship(Friendship FriendshipToAdd);
    Task<ICollection<Friendship>> GetAllFriendshipsAsync();
    Task DeleteFriendship(Friendship toDelete);
    Task UpdateFriendshipStatus(Friendship toUpdate);
}