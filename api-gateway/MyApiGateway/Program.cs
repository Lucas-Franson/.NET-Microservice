using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication()
    .AddJwtBearer("Bearer", options => {
            options.Authority = "https://localhost:5001";
            options.Audience = "api1";
        });

builder.Configuration.AddJsonFile("ocelot.json");
builder.Services
    .AddOcelot()
    .AddConsul();

var app = builder.Build();

app.UseAuthentication();
app.UseOcelot().Wait();

app.MapGet("/", () => "Hello World!");

app.Run();
