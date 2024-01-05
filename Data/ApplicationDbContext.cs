using Microsoft.EntityFrameworkCore;

namespace ShoppingListDemo.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<ShoppingSchedule>? ShoppingSchedules { get; set; }

    public DbSet<ShoppingCategory>? ShoppingCategories { get; set; }

    public DbSet<ShoppingItem>? ShoppingItems { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingSchedule>(entity =>
        {
            entity.HasIndex(e => e.Day).IsUnique();
        });
    }
}