namespace UrlShortener.DTOs;

public class ShortenUrlDTO
{
    public int CurrentUserId { get; set; }

    public string OriginalUrl { get; set; }
}
