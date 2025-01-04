using System.Security.Claims;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ZyphCare.Api.Common;
using ZyphCare.Api.Common.Extensions;
using ZyphCare.Common.Extensions;
using ZyphCare.EntityFramework.Sqlite;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

var sqliteConnectionString = configuration.GetConnectionString("Sqlite")!;

services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = configuration.GetValue<string>("Authentication:Authority");
        options.Audience = configuration.GetValue<string>("Authentication:Audience");
        options.RequireHttpsMetadata = false;

        options.Events = new JwtBearerEvents
            {
                OnTokenValidated = (context) =>
                {
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
            .AddSwagger()
            .AddFastEndpointsAssembly<Program>();
    });

services.AddFastEndpoints();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

    
var app = builder.Build();
app.UseAuthorization();
app.UseZyphCareApi();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUi();
}

app.Run();
