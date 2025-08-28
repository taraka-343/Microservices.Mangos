using mangos.services.AuthApi.Data;
using mangos.services.AuthApi.Models;
using mangos.services.AuthApi.services;
using mangos.services.AuthApi.services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// in this we are telling that we are using entity framework for identity provider , and
// also we are using tokens 
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
//jwt token configuration,data will set automaticcaly to JwtOptions class
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("TokenSettings:JwtOptions"));
//add IAuthService to AuthService
builder.Services.AddScoped<IAuthService, AuthService>();
//add IJwtTokenGenerator to JwtTokenGenerator
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
doPendingMigration();

app.Run();
void doPendingMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}
