namespace UrlShortener.Models;

public class ShortUrlDbModel
{
    public int Id { get; set; }
    public string OriginalUrl { get; set; }
    public string ShortUrl { get; set; }
    public DateTime CreatedDate { get; set; }
    public int CreatedBy { get; set; }
    public UserDbModel Creator { get; set; }
}
