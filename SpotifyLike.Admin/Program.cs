using SpotifyLike.Repository;
using Microsoft.EntityFrameworkCore;
using SpotifyLike.Application.Admin.Profile;
using SpotifyLike.Application.Admin;
using SpotifyLike.Repository.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Spotify.Application.Streaming;
using SpotifyLike.Application.Streaming;
using Spotify.Application.Streaming.Profile;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SpotifyLikeAdminContext>(c =>
{
    c.UseLazyLoadingProxies()
     .UseSqlServer(builder.Configuration.GetConnectionString("SpotifyConnectionAdmin"));

});

builder.Services.AddDbContext<SpotifyLikeContext>(c =>
{
    c.UseLazyLoadingProxies()
     .UseSqlServer(builder.Configuration.GetConnectionString("SpotifyConnection"));

});

builder.Services.AddAutoMapper(typeof(UsuarioAdminProfile).Assembly);

builder.Services.AddAutoMapper(typeof(MusicaProfile));

builder.Services.AddScoped<UsuarioAdminRepository>();
builder.Services.AddScoped<UsuarioAdminService>();
builder.Services.AddScoped<BandaRepository>();
builder.Services.AddScoped<BandaService>();
builder.Services.AddScoped<MusicaRepository>();
builder.Services.AddScoped<MusicaService>();
builder.Services.AddScoped<AlbumRepository>();
builder.Services.AddScoped<AlbumService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
