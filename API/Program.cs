using API;
using Bussines;
using DataAcces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Models.Contracts;
using Models.Dtos;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuracion de cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
               // Reemplaza con el origen permitido
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var secretKey = builder.Configuration.GetValue<string>("Jwt:SecretKey");
int expires = builder.Configuration.GetValue<int>("Jwt:Expires");
byte[] secretKeyBytes = Encoding.ASCII.GetBytes(secretKey);
builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    ).AddJwtBearer(
    x => {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
            ValidateIssuer = false,
            ValidateAudience = false

        };
    });


builder.Services.AddDbContext<BDContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Api_Crud")).UseLazyLoadingProxies());

builder.Services.AddScoped<IAccessToken, AccessToken>();
builder.Services.AddSingleton<TokenFactory>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICliente, Cliente>();
builder.Services.AddScoped<ILog, Log>();

var app = builder.Build();

//usar la politica de cros creada para usar en el front
app.UseCors("CorsPolicy");

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
