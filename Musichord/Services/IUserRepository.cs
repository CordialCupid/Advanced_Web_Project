namespace Musichord.Services;
using Musichord.Models.Entities;



public interface IUserRepository
{
    Task<ApplicationUser?> ReadByUsernameAsync(string username);
    Task<ApplicationUser?> ReadByHandleAsync(string handle);
}