
using Microsoft.EntityFrameworkCore;

public class PostgresContext : DbContext {
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options) { }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        
        modelBuilder.Entity<UserEntity>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<RoleEntity>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.ToTable("UserRoles"));

        modelBuilder
            .Entity<RoleEntity>()
            .HasData(
                new RoleEntity { Id = 1, Name = "Admin", Description = "Administrator role" },
                new RoleEntity { Id = 2, Name = "User", Description = "Regular user role" }
            );
        
    }
}