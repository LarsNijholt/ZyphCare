using ZyphCare.EntityFramework.Modules.Users;
using ZyphCare.Users.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


services.AddZyphCareUsers(x => x.UseEntityFrameworkCore());

    
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
