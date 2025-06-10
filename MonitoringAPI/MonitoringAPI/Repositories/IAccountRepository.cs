public interface IAccountRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User> RegisterAsync(User user, string password);
    bool IsPasswordStrong(string password);
    Task<bool> CheckPasswordAsync(User user, string password);

}
