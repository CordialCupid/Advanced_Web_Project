namespace Musichord.Models.Entities;

public interface IFriendshipRepo
{
    Task<Friendship> CreateFriendship(ApplicationUser sender, ApplicationUser receiver);
    Task<ICollection<Friendship>> GetAllFriendshipsAsync();
    Task<ICollection<string>> GetAllFriendsHandlesAsync(string user);
    Task DeleteFriendship(string sender, string receiver);
    Task UpdateFriendshipStatus(string sender, string receiver);
}