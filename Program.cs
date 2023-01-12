using System.Text;
using Authenticate;
using ShoppingCart.Business;
using ShoppingCart.Repository;
using ShoppingCart.Repository.Interfaces;
using ShoppingCart.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
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
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Repository
builder.Services.AddScoped<IAccoutRepository, AccoutRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();

// Business
builder.Services.AddTransient<AccoutBusiness>();
builder.Services.AddTransient<CartBusiness>();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// tempo de vida do serviço
builder.Services.AddTransient<TokenService>(); // sempre criar um novo
//builder.Services.AddScoped(); // enquanto a requisição durar
//builder.Services.AddSingleton(); // 1 por aplicação

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();