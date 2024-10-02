using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private IConfiguration _config;
    public LoginController(IConfiguration config) 
    {
        _config = config;
    }

    [HttpPost]
    public IActionResult Post([FromBody] LoginRequestViewModel loginRequest)
    {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        if (loginRequest.Username != "test" || loginRequest.Password != "password") {
            return Unauthorized();
        }
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            null,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        var token =  new JwtSecurityTokenHandler().WriteToken(Sectoken);

        return Ok(token);
    }
}