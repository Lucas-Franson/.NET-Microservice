
using Microsoft.EntityFrameworkCore;

public class PostgresContext : DbContext {
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options) { }

    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        
        modelBuilder
            .Entity<ProductEntity>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<CategoryEntity>()
            .HasData(
                new CategoryEntity { Id = 1, Name = "Category 1", Active = true, UpdatedAt = DateTime.UtcNow, CreatedAt = DateTime.UtcNow },
                new CategoryEntity { Id = 2, Name = "Category 2", Active = true, UpdatedAt = DateTime.UtcNow, CreatedAt = DateTime.UtcNow },
                new CategoryEntity { Id = 3, Name = "Category 3", Active = true, UpdatedAt = DateTime.UtcNow, CreatedAt = DateTime.UtcNow }
            );
        
    }
}