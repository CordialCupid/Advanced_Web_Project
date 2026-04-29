namespace Musichord.Services;
using Musichord.Models.Entities;



public interface IUserRepository
{
    Task<ApplicationUser?> ReadByUsernameAsync(string username);
    Task<ApplicationUser?> ReadByHandleAsync(string handle);

    Task<ICollection<ApplicationUser>> ReadAllAsync();
    Task<ICollection<ApplicationUser>> ReadAllExceptAsync(string id);
    Task<ICollection<string>> ReadAllHandlesExceptUserAsync(string user);
}