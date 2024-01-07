using Microsoft.EntityFrameworkCore;

namespace ShoppingListDemo.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<ScheduledShoppingItem> ScheduledShoppingItems { get; set; } = null!;

    public DbSet<ShoppingCategory> ShoppingCategories { get; set; } = null!;

    public DbSet<ShoppingItem> ShoppingItems { get; set; } = null!;

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
            entity.HasIndex(x => x.Day);
            entity.HasIndex(nameof(ScheduledShoppingItem.Day), nameof(ScheduledShoppingItem.ShoppingItemId))
                .IsUnique();
        });
    }
}