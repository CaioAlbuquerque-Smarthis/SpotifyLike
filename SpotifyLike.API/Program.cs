using IdentityServer4.AccessTokenValidation;
using Microsoft.EntityFrameworkCore;
using Spotify.Application.Conta;
using Spotify.Application.Conta.Profile;
using Spotify.Application.Streaming;
using SpotifyLike.Application.Conta;
using SpotifyLike.Repository;
using SpotifyLike.Repository.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SpotifyLikeContext>(c =>
{
    c.UseLazyLoadingProxies()
     .UseSqlServer(builder.Configuration.GetConnectionString("SpotifyConnection"));

});

builder.Services.AddAutoMapper(typeof(UsuarioProfile).Assembly);

builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:7064";
                    options.ApiName = "spotifyLike-api";
                    options.ApiSecret = "SpolitifyLikeSecret";
                    options.RequireHttpsMetadata = true;
                });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("spotifylike-role-user", p =>
    {
        p.RequireClaim("role", "spotifylike-user");
    });
});

//Repositories
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<PlanoRepository>();
builder.Services.AddScoped<BandaRepository>();

//Services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<BandaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
