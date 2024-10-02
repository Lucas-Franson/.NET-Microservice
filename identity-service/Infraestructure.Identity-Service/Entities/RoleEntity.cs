
using System.ComponentModel.DataAnnotations;

public class RoleEntity : DefaultEntity
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(250)]
    public string Description { get; set; }

    public ICollection<UserEntity> Users { get; set; }
}