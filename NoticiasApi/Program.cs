using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NoticiasApi.Helpers;
using NoticiasApi.Models.Entities;
using NoticiasApi.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    x =>
    {
        var issuer = builder.Configuration.GetSection("JWTBearer").GetValue<string>("Issuer");
        var secret = builder.Configuration.GetSection("JWTBearer").GetValue<string>("Secret");

        var audience = builder.Configuration.GetSection("JWTBearer").GetValue<string>("Audience");


        x.TokenValidationParameters = new()
        {
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret ?? "")),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true
        };

    });

var contectionString = builder.Configuration.GetConnectionString("NoticiasConectionString");
builder.Services.AddDbContext<ItesrcneOctavoContext>(x =>
x.UseMySql(contectionString, ServerVersion.AutoDetect(contectionString)));

builder.Services.AddSingleton<JwtHelper>();
builder.Services.AddTransient(typeof(IRepository<>),typeof(Repository<>));

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

app.Run();
