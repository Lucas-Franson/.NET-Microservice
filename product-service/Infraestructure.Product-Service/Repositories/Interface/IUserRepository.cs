
public interface IUserRepository
{
    Task<List<UserEntity>> GetUsers();
}