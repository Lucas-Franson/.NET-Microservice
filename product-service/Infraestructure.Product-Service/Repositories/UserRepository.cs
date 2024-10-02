

public class UserRepository : IUserRepository
{
    private readonly IConsulHttpClient _consulHttpClient;

    public UserRepository(IConsulHttpClient consulHttpClient)
    {
        _consulHttpClient = consulHttpClient;
    }

    public async Task<List<UserEntity>> GetUsers()
    {
        var basket = await _consulHttpClient.GetAsync<List<UserEntity>>("Identity-Service", $"/api/user");

        return basket;
    }
}