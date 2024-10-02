

using Consul;

public class ConsulRegistrationService : IHostedService {

    private readonly IConsulClient _consulClient;
    private readonly IConfiguration _configuration;
    private string _serviceId = string.Empty;

    public ConsulRegistrationService (IConsulClient consulClient, IConfiguration configuration) {
        _consulClient = consulClient;
        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var service = _configuration.GetValue<string>("ServiceConfiguration:ServiceName");
        var host = _configuration.GetValue<string>("ServiceConfiguration:Host");
        var port = _configuration.GetValue<string>("ServiceConfiguration:Port");

        _serviceId = $"{service}-{Guid.NewGuid()}";
        Console.WriteLine($"Service Name: {service}");
        Console.WriteLine($"Service Host: {host}");
        Console.WriteLine($"Service Port: {port}");
        var tagList = new string[] { service };
        var register = new AgentServiceRegistration() {
            ID = _serviceId,
            Name = service,
            Port = Convert.ToInt32(port),
            Address = host,
            Tags = tagList
        };

        await _consulClient.Agent.ServiceRegister(register, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _consulClient.Agent.ServiceDeregister(_serviceId, cancellationToken);
    }
}