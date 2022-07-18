using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddEntityFrameworkMySQL().AddDbContext<DbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000");
                      });
});

app.UseCors(MyAllowSpecificOrigins);

app.UseExtraRoutes();

app.MapGet("/", () => "Hello World!");

app.Run();
