

public class UserRepository : IUserRepository
{
    private readonly PostgresContext _context;

    public UserRepository(PostgresContext context)
    {
        _context = context;
    }

    public void CreateUser(UserEntity user)
    {
        throw new NotImplementedException();
    }

    public void DeleteUser(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserEntity> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public UserEntity GetUserById(int id)
    {
        return _context.Set<UserEntity>().Find(id);
    }

    public UserEntity GetUserByUsername(string username)
    {
        throw new NotImplementedException();
    }

    public void UpdateUser(UserEntity user)
    {
        throw new NotImplementedException();
    }
}