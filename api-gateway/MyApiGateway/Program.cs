using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceDiscovery();
builder.Configuration.AddJsonFile("ocelot.json");
builder.Services
    .AddAuthentication()
    .AddJwtBearer("Bearer", options => {
            options.Authority = "https://localhost:5001";
            options.Audience = "api1";
        });
builder.Services.AddOcelot();

var app = builder.Build();

app.UseOcelot().Wait();

app.MapGet("/", () => "Hello World!");

app.Run();
