using System.Security.Claims;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ZyphCare.Api.Common;
using ZyphCare.Api.Common.Extensions;
using ZyphCare.Common.Extensions;
using ZyphCare.EntityFramework.Sqlite;
using ZyphCare.EntityFramework.Units.Users;
using ZyphCare.Users.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

var sqliteConnectionString = configuration.GetConnectionString("Sqlite")!;
EndpointSecurityOptions.DisableSecurity();

services
    .AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.Authority = configuration.GetValue<string>("Authentication:Authority");
        options.Audience = configuration.GetValue<string>("Authentication:Audience");
        options.RequireHttpsMetadata = false;

        options.Events = new JwtBearerEvents
            {
                OnTokenValidated = (context) =>
                {
                    //Simplification of the Elsa permissions by granting access to everything
                    var identity = context.Principal.Identity as ClaimsIdentity;
                    identity?.AddClaim(new Claim("permissions", PermissionNames.All));
                    return Task.CompletedTask;
                }
            };
    });
services.AddAuthorization();

services
    .AddZyphCareUnits(units =>
    {
        units
            .AddZyphCareUsers(x => x.UseEntityFrameworkCore(unit => unit.UseSqlite(sqliteConnectionString)))
            .AddSwagger()
            .AddFastEndpointsAssembly<Program>();
    });

services.AddFastEndpoints();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

    
var app = builder.Build();
app.UseAuthorization();
app.UseZyphCareAPI();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.Run();
