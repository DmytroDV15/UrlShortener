namespace UrlShortener.DTOs;

public class ShortUrlDTO
{
    public int Id { get; set; }
    public string OriginalUrl { get; set; }
    public string ShortUrl { get; set; }
}
