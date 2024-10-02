using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase {
    private readonly IUserService _userService;
    private readonly IKafkaProducer _kafkaProducer;

    public UserController(IUserService userService, IKafkaProducer kafkaProducer) {
        _userService = userService;
        _kafkaProducer = kafkaProducer;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserEntity>> GetUserById(int id) {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserEntity>>> GetAllUsers() {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<UserEntity>> CreateUser(UserEntity user) {
        var createdUser = await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut]
    public async Task<ActionResult<UserEntity>> UpdateUser(UserEntity user) {
        var updatedUser = await _userService.UpdateUserAsync(user);
        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id) {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }

    [HttpPost("BuyProduct")]
    public async Task<ActionResult> BuyProduct() {
        _kafkaProducer.ProduceAsync("buy-product", "User bought a product");
        return Ok();
    }

}