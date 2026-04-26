namespace Musichord.Models.Entities;

public interface IFriendshipRepo
{
    Task<ICollection<Friendship>> GetAcceptedFriendshipsAsync(string userId);
    Task<Friendship> CreateFriendship(ApplicationUser sender, ApplicationUser receiver);
    //Task UpdateFriendship(ApplicationUser sender, ApplicationUser receiver);
    Task<ICollection<Friendship>> GetAllFriendshipsAsync();
    Task<ICollection<Friendship>> GetUnacceptedFriendshipsAsync(string userId);
}