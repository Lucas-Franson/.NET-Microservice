
using System.ComponentModel.DataAnnotations;

public class UserEntity : DefaultEntity
{
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    [MaxLength(100)]
    public string Password { get; set; }

    [Required]
    [MaxLength(100)]
    public string Email { get; set; }

    public ICollection<RoleEntity> Roles { get; set; }
}