
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class CategoryEntity : DefaultEntity {
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    [DefaultValue(false)]
    public bool Active { get; set; }
    public List<ProductEntity> Products { get; set; }
}