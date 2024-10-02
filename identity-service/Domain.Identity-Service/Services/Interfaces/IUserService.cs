
public interface IUserService
{
    Task<UserEntity> GetUserByIdAsync(int id);
    Task<List<UserEntity>> GetAllUsersAsync();
    Task<UserEntity> CreateUserAsync(UserEntity user);
    Task<UserEntity> UpdateUserAsync(UserEntity user);
    Task DeleteUserAsync(int id);
}