using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryWebApi.Data.LibraryContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("LibraryWebApi")));

// Add services to the container.
builder.Services.AddDbContext<LibraryWebApi.Data.LibraryContext>();
builder.Services.AddControllers();

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
