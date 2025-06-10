using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User> RegisterAsync(User user, string password)
    {
        if (!IsPasswordStrong(password))
        {
            throw new ArgumentException("Password must be at least 8 characters and contain uppercase, lowercase, digit, and special character.");
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public bool IsPasswordStrong(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) return false;
        if (password.Length < 8) return false;

        bool hasUpper = Regex.IsMatch(password, "[A-Z]");
        bool hasLower = Regex.IsMatch(password, "[a-z]");
        bool hasDigit = Regex.IsMatch(password, @"\d");
        bool hasSpecial = Regex.IsMatch(password, @"[\W_]");

        return hasUpper && hasLower && hasDigit && hasSpecial;
    }


    public Task<bool> CheckPasswordAsync(User user, string password)
    {
        return Task.FromResult(BCrypt.Net.BCrypt.Verify(password, user.PasswordHash));
    }

}
