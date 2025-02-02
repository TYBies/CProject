public interface IUserService
{
    Task CreateUserAsync(string username, string password);    
    Task<string> AuthenticateUserAsync(string username, string password);
}
