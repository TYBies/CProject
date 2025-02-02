using FootballMatches.API.Models;

namespace FootballMatches.API.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);        
    }
}
