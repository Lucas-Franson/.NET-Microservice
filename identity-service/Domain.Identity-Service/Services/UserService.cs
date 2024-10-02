
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService {
    private readonly PostgresContext _context;

    public UserService(PostgresContext context) {
        _context = context;
    }

    public async Task<UserEntity> GetUserByIdAsync(int id) {
        return await _context.Users.FindAsync(id);
    }

    public  Task<List<UserEntity>> GetAllUsersAsync() {
        return _context.Users.ToListAsync();
    }

    public async Task<UserEntity> CreateUserAsync(UserEntity user) {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<UserEntity> UpdateUserAsync(UserEntity user) {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUserAsync(int id) {
        var user = await _context.Users.FindAsync(id);
        if (user != null) {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}