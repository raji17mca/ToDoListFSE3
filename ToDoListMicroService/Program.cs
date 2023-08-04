using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using ToDoListMicroService.Repository;
using ToDoListMicroService.Services;
using ToDoListMicroService.DataBaseConfig;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using MassTransit;
using ToDoListMicroService.Consumer;
using MongoDB.Driver;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

// Add services to the container.

builder.Services.Configure<ToDoListDataBaseSettings>(builder.Configuration.GetSection(nameof(ToDoListDataBaseSettings)));

builder.Services.AddSingleton<IToDoListDataBaseSettings>(sp =>
              sp.GetRequiredService<IOptions<ToDoListDataBaseSettings>>().Value);

builder.Services.AddScoped<IToDoListCommandRepository, ToDoListCommandRepository>();
builder.Services.AddScoped<IToDoListQueryRepository, ToDoListQueryRepository>();
builder.Services.AddScoped<IToDoListService, ToDoListService>();
builder.Services.AddScoped<ITokenService, TokenService>();

////builder.Services.AddSingleton<IMongoClient>(s =>
////    new MongoClient(builder.Configuration.GetSection("ToDoListDataBaseSettings").GetSection("ConnectionString").Value)
////);

builder.Services.AddHttpContextAccessor();


var configSection = builder.Configuration.GetSection("ServiceBus");
var connectionUri = configSection.GetSection("ConnectionUri").Value;
var usename = configSection.GetSection("Username").Value;
var password = configSection.GetSection("Password").Value;
var queueName = configSection.GetSection("QueueName").Value;

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(connectionUri), h =>
        {
            h.Username(usename);
            h.Password(password);
        });
        cfg.ReceiveEndpoint(queueName, ep =>
        {
            ep.ConfigureConsumer<UserConsumer>(context);
        });
    });
});

builder.Services.AddControllers();
//MediatR
builder.Services.AddMediatR(typeof(Program).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

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
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo List Services"));
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
