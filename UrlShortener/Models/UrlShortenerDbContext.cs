using Microsoft.EntityFrameworkCore;
using UrlShortener.Enums;

namespace UrlShortener.Models;

public class UrlShortenerDbContext : DbContext
{
    public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options) : base(options)
    {

    }

    public DbSet<UserDbModel> Users { get; set; }
    public DbSet<ShortUrlDbModel> ShortsUrls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDbModel>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<UserDbModel>()
           .HasData(new UserDbModel
           {
               Id = 999,
               Login = "AdminUser",
               Password = "$2a$11$e1D7RwXS73L93r2qe.Re3O59iEOXTpSarM0J1XzqVT6yvPPTrT3la", // Password: zxcvbnm
               Role = UserRole.Admin
           });

        modelBuilder.Entity<ShortUrlDbModel>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<ShortUrlDbModel>()
            .HasOne(u => u.Creator)
            .WithMany(c => c.ShortsUrls)
            .HasForeignKey(u => u.CreatedBy);
    }
}
