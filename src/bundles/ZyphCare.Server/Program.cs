using FastEndpoints;
using ZyphCare.Api.Common.Extensions;
using ZyphCare.Common.Extensions;
using ZyphCare.EntityFramework.Sqlite;
using ZyphCare.EntityFramework.Units.Users;
using ZyphCare.Users.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

var sqliteConnectionString = configuration.GetConnectionString("Sqlite")!;


services
    .AddZyphCareUnits(units =>
    {
        units
            .AddZyphCareUsers(x => x.UseEntityFrameworkCore(unit => unit.UseSqlite(sqliteConnectionString)))
            .AddSwagger()
            .AddFastEndpointsAssembly<Program>();
    });

services.AddFastEndpoints();


    
var app = builder.Build();
app.UseFastEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.Run();
