using Consul;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<PostgresContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ProductServiceConnection"));
});

var consultHost = builder.Configuration.GetValue<string>("ConsulConfiguration:Host");

builder.Services.AddSingleton<IHostedService, ConsulRegistrationService>();
builder.Services.AddSingleton<IConsulClient>(p => new ConsulClient(config => {
    config.Address = new Uri(consultHost);
}));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
