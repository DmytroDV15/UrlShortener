namespace UrlShortener.DTOs;

public class UrlInfoDTO
{
    public int Id { get; set; }
    public string OriginalUrl { get; set; }
    public string ShortUrl { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
}
