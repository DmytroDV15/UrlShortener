using UrlShortener.DTOs;

namespace UrlShortener.Interfaces;

public interface IUserService
{
    public Task<UserDTO> LoginUser(LoginDTO loginDTO);
}
