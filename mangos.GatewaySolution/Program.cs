using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//configure ocelot
builder.Services.AddOcelot();
//validate JWT token with secrete, issuer, audiance provided by api owner
//This ensures that before any controller runs, the system:
//Extracts the token from Authorization header
//Validates it using JwtBearerOptions
//Parses it and stores the claims in HttpContext.User
//All this happens automatically by middleware before your controller executes
var secret = builder.Configuration.GetValue<string>("TokenSettings:Secret");
var issuer = builder.Configuration.GetValue<string>("TokenSettings:Issuer");
var audiance = builder.Configuration.GetValue<string>("TokenSettings:Audience");

var key = Encoding.ASCII.GetBytes(secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audiance
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();


app.MapGet("/", () => "Hello World!");
app.UseOcelot();
app.Run();
