
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProductEntity : DefaultEntity {
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    [DefaultValue(0)]
    public int Stock { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public CategoryEntity Category { get; set; }
    [DefaultValue(false)]
    public bool Active { get; set; }
}