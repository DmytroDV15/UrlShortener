using Microsoft.AspNetCore.Mvc;
using UrlShortener.DTOs;
using UrlShortener.Interfaces;

namespace UrlShortener.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UrlController : Controller
{
    private readonly IUrlService _urlService;

    public UrlController(IUrlService urlService)
    {
        _urlService = urlService;
    }

    [HttpPost("shorten")]
    public async Task<IActionResult> ShortenUrl([FromBody] ShortenUrlDTO request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.OriginalUrl))
            {
                return BadRequest("URL cannot be empty.");
            }

            await _urlService.AddUrls(request.OriginalUrl, request.CurrentUserId);
            return Ok(new { Message = "URL added successfully!" });
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(new { ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { ex.Message });
        }
        catch
        {
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }

    [HttpDelete("{urlId}/{currentUser}")]
    public async Task<IActionResult> DeleteUrl(int urlId, int currentUser)
    {
        await _urlService.DeleteUrl(urlId, currentUser);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var urls = await _urlService.GetAllUrls();

        return Ok(urls);
    }

    [HttpGet("info/{urlId}")]
    public async Task<IActionResult> UrlInfo(int urlId)
    {
        var info = await _urlService.UrlInfo(urlId);

        return Ok(info);
    }
}
