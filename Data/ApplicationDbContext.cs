using Microsoft.EntityFrameworkCore;

namespace ShoppingListDemo.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<ScheduledShoppingItem> ScheduledShoppingItems { get; set; }

    public DbSet<ShoppingCategory> ShoppingCategories { get; set; }

    public DbSet<ShoppingItem> ShoppingItems { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingCategory>(entity =>
        {
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasIndex(e => e.Order).IsUnique();
        });

        modelBuilder.Entity<ScheduledShoppingItem>(entity =>
        {
            entity.HasIndex(e => e.Day).IsUnique();
        });
    }
}