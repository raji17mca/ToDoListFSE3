using AuthenticationMicroService.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using ToDoListMicroService.DataBaseConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

// Add services to the container.
builder.Services.Configure<UserDataBaseSettings>(builder.Configuration.GetSection(nameof(UserDataBaseSettings)));

builder.Services.AddSingleton<IUserDataBaseSettings>(sp =>
              sp.GetRequiredService<IOptions<UserDataBaseSettings>>().Value);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddHttpContextAccessor();

// Mass Transit
var configSection = builder.Configuration.GetSection("ServiceBus");
var connectionUri = configSection.GetSection("ConnectionUri").Value;
var usename = configSection.GetSection("Username").Value;
var password = configSection.GetSection("Password").Value;

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(connectionUri), h =>
        {
            h.Username(usename);
            h.Password(password);
        });
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
       //// ValidIssuer = "http://localhost:5265",
       //// ValidAudience = "http://localhost:5265",
       //// IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
    };
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "User Services"));
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
