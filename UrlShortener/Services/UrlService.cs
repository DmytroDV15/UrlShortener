using Microsoft.EntityFrameworkCore;
using UrlShortener.DTOs;
using UrlShortener.Enums;
using UrlShortener.Interfaces;
using UrlShortener.Models;

namespace UrlShortener.Services;

public class UrlService : IUrlService
{
    private readonly UrlShortenerDbContext _dbContext;

    public UrlService(UrlShortenerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddUrls(string urls, int currentUserId)
    {
        var user = await GetUserById(currentUserId) ?? throw new ArgumentNullException("User is not found");

        var existingUrl = await _dbContext.ShortsUrls.FirstOrDefaultAsync(x => x.OriginalUrl == urls);

        if (existingUrl != null)
        {
            throw new InvalidOperationException("Url already added");
        }

        existingUrl = new ShortUrlDbModel
        {
            OriginalUrl = urls,
            ShortUrl = GenerateShortUrl(),
            CreatedDate = DateTime.Now,
            CreatedBy = user.Id
        };

        _dbContext.ShortsUrls.Add(existingUrl);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUrl(int id, int currentUserId)
    {
        var user = await GetUserById(currentUserId) ?? throw new ArgumentNullException("User is not found"); ;

        var existingUrl = await _dbContext.ShortsUrls.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Url not found");
        if (user.Id != existingUrl.CreatedBy && user.Role != UserRole.Admin)
        {
            throw new UnauthorizedAccessException("No permission");
        }

        _dbContext.ShortsUrls.Remove(existingUrl);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<ShortUrlDTO>> GetAllUrls()
    {
        var urls = await _dbContext.ShortsUrls.Select(url => new ShortUrlDTO
        {
            Id = url.Id,
            OriginalUrl = url.OriginalUrl,
            ShortUrl = url.ShortUrl,
        }).ToListAsync();

        return urls;
    }

    public async Task<UrlInfoDTO> UrlInfo(int id)
    {
        var infoUrl = await _dbContext.ShortsUrls.FirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception("Url not found");

        return new UrlInfoDTO
        {
            Id = infoUrl.Id,
            OriginalUrl = infoUrl.OriginalUrl,
            ShortUrl = infoUrl.ShortUrl,
            CreatedBy = infoUrl.CreatedBy,
            CreatedDate = infoUrl.CreatedDate
        };
    }

    private static string GenerateShortUrl()
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private async Task<UserDbModel?> GetUserById(int currentUserId)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == currentUserId);
    }
}
