using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FootballMatches.API.Interfaces;
using FootballMatches.API.Models;
using System.Security.Cryptography;

namespace FootballMatches.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtSecret;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IConfiguration configuration, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _jwtSecret = configuration["Jwt:Secret"] ?? throw new ArgumentNullException("Jwt:Secret", "JWT Secret is not configured.");
            _jwtIssuer = configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer", "JWT Issuer is not configured.");
            _jwtAudience = configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience", "JWT Audience is not configured.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task CreateUserAsync(string username, string password)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByUsernameAsync(username);
                if (existingUser != null)
                {
                    _logger.LogWarning("Attempt to create user failed. Username '{Username}' already exists.", username);
                    throw new InvalidOperationException("Username already exists.");
                }

                var salt = BCrypt.Net.BCrypt.GenerateSalt();
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

                var user = new User
                {
                    Username = username,
                    PasswordHash = hashedPassword,
                    CreatedAt = DateTime.Now,
                    Salt = salt
                };

                await _userRepository.AddUserAsync(user);
                _logger.LogInformation("User '{Username}' created successfully.", username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the user '{Username}'.", username);
                throw new ApplicationException("An error occurred while creating the user.", ex);
            }
        }

        public async Task<string> AuthenticateUserAsync(string username, string password)
        {
            try
            {
                var user = await _userRepository.GetUserByUsernameAsync(username);

                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    _logger.LogWarning("Authentication failed for user '{Username}'. Invalid username or password.", username);
                    throw new UnauthorizedAccessException("Invalid username or password.");
                }

                _logger.LogInformation("User '{Username}' authenticated successfully.", username);
                return GenerateJwtToken(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while authenticating the user '{Username}'.", username);
                throw new ApplicationException("An error occurred while authenticating the user.", ex);
            }
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.Name, user.Username),
                    new (ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(3),
                Issuer =_jwtIssuer,
                Audience = _jwtAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }        
    }
}