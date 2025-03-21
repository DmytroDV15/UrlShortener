using Microsoft.AspNetCore.Mvc;
using UrlShortener.DTOs;
using UrlShortener.Interfaces;

namespace UrlShortener.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginController : Controller
{
    private readonly IUserService _loginService;

    public LoginController(IUserService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO user)
    {
        var result = await _loginService.LoginUser(user);
        return Ok(result);
    }
}
