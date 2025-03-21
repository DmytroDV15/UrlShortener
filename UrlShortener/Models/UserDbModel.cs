using UrlShortener.Enums;

namespace UrlShortener.Models;

public class UserDbModel
{
    public int Id { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public UserRole Role { get; set; }

    public List<ShortUrlDbModel> ShortsUrls { get; set; } = [];
}
