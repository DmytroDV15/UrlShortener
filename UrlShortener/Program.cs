using Microsoft.EntityFrameworkCore;
using UrlShortener.Interfaces;
using UrlShortener.Models;
using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<UrlShortenerDbContext>(e => e.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowAnyOrigin();


        });
});

builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUrlService, UrlService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty;
    });
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowFrontend");

app.MapGet("api/{shortUrl}", async (string shortUrl, UrlShortenerDbContext dbContex) =>
{
    var shortenedUrl = await dbContex.ShortsUrls.FirstOrDefaultAsync(s => s.ShortUrl == shortUrl);

    if (shortenedUrl == null)
    {
        return Results.NotFound();
    }

    return Results.Redirect(shortenedUrl.OriginalUrl);
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.UseHttpsRedirection();

app.Run();
