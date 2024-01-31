using Microsoft.EntityFrameworkCore;
using TestApp;
using TestApp.Database;
using TestApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddGrpcServer();
builder.Services.AddDbContext<StoreDbContext>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.RegisterGrpcServices();
app.Services.MigrateDatabase();

app.Run();



