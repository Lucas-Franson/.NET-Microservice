
public interface IConsulHttpClient
{
    Task<T> GetAsync<T>(string serviceName, string requestUri);
}