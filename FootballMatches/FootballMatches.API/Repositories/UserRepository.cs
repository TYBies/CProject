using FootballMatches.API.Data;
using FootballMatches.API.Interfaces;
using FootballMatches.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballMatches.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            try
            {
                _logger.LogInformation("Fetching user with username: {Username}", username);
               var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);

                if (user == null)
                {
                    _logger.LogWarning("User with username '{Username}' not found.", username);
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the user with username '{Username}'.", username);
                throw new ApplicationException("An error occurred while fetching the user.", ex);
            }
        }

        public async Task AddUserAsync(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                _logger.LogInformation("Adding user with username: {Username}", user.Username);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("User with username '{Username}' added successfully.", user.Username);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error occurred while adding user with username '{Username}'.", user.Username);
                throw new ApplicationException("An error occurred while adding the user.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the user with username '{Username}'.", user.Username);
                throw new ApplicationException("An error occurred while adding the user.", ex);
            }
        }
    }
}
