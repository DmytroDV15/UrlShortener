using UrlShortener.DTOs;

namespace UrlShortener.Interfaces;

public interface IUrlService
{
    public Task AddUrls(string url, int currentUserId);
    public Task DeleteUrl(int id, int currentUserId);
    public Task<List<ShortUrlDTO>> GetAllUrls();
    public Task<UrlInfoDTO> UrlInfo(int id);
}
