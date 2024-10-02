
using Consul;
using Newtonsoft.Json;

public class ConsulHttpClient : IConsulHttpClient
{
    private readonly HttpClient _client;
    private IConsulClient _consulclient;

    public ConsulHttpClient(HttpClient client, IConsulClient consulclient)
    {
        _client = client;
        _consulclient = consulclient;
    }

    public async Task<T> GetAsync<T>(string serviceName, string requestUri)
    {
        var uri = await GetRequestUriAsync(serviceName, requestUri);

        var response = await _client.GetAsync(uri);

        if (!response.IsSuccessStatusCode)
        {
            return default(T);
        }

        var content = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(content);
    }

    private async Task<Uri> GetRequestUriAsync(string serviceName, string uri)
    {
        //Get all services registered on Consul
        var allRegisteredServices = await _consulclient.Agent.Services();

        //Get all instance of the service went to send a request to
        var registeredServices = allRegisteredServices.Response?.Where(s => s.Value.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase)).Select(x => x.Value).ToList();

        //Get a random instance of the service
        var service = GetRandomInstance(registeredServices);

        if (service == null)
        {
            throw new Exception($"Consul service: '{serviceName}' was not found.");
        }

        // var uriBuilder = new UriBuilder(uri)
        // {
        //     Host = service.Address,
        //     Port = service.Port
        // };

        return new Uri($"http://{service.Address}:{service.Port}{uri}");
    }

    private AgentService GetRandomInstance(IList<AgentService> services)
    {
        Random _random = new Random();

        AgentService servToUse = null;

        servToUse = services[_random.Next(0, services.Count)];

        return servToUse;
    }
}