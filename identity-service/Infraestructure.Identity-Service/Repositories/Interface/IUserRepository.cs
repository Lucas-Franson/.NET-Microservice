
public interface IUserRepository
{
    UserEntity GetUserById(int id);
    UserEntity GetUserByUsername(string username);
    void CreateUser(UserEntity user);
    void UpdateUser(UserEntity user);
    void DeleteUser(int id);
    IEnumerable<UserEntity> GetAllUsers();
}