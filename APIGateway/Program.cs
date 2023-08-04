using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange:true);


builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();
////builder.Services.AddCors(options =>
////{
////    options.AddDefaultPolicy(
////        builder =>
////        {
////            builder
////            .AllowAnyOrigin()
////            .AllowAnyMethod()
////            .AllowAnyHeader();
////        });
////});

app.MapGet("/", () => "Hello World!");
app.MapControllers();

await app.UseOcelot();
app.Run();
