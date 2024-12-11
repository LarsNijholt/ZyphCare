using ZyphCare.EntityFramework.Modules.Users;
using ZyphCare.EntityFramework.Sqlite;
using ZyphCare.Extensions;
using ZyphCare.Users.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

var sqliteConnectionString = configuration.GetConnectionString("Sqlite")!;


services.AddZyphCareUnits(units =>
{
    units.AddZyphCareUsers(x => x.UseEntityFrameworkCore(unit => unit.UseSqlite(sqliteConnectionString)));
});

    
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
