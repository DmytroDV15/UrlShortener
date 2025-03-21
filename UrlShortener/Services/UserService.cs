using Microsoft.EntityFrameworkCore;
using UrlShortener.DTOs;
using UrlShortener.Enums;
using UrlShortener.Interfaces;
using UrlShortener.Models;

namespace UrlShortener.Services;

public class UserService : IUserService
{
    private readonly UrlShortenerDbContext _dbContext;

    public UserService(UrlShortenerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDTO> LoginUser(LoginDTO loginDTO)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Login == loginDTO.Login);

        if (user != null)
        {
            if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
            {
                throw new Exception("Invalid login or password.");
            }

            return new UserDTO
            {
                Id = user.Id,
                Login = user.Login
            };
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(loginDTO.Password);
        user = new UserDbModel
        {
            Login = loginDTO.Login,
            Password = hashedPassword,
            Role = UserRole.Ordinary
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return new UserDTO
        {
            Id = user.Id,
            Login = user.Login
        };
    }
}
