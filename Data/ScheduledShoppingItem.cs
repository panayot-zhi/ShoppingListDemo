using System.ComponentModel.DataAnnotations;

namespace ShoppingListDemo.Data;

public class ScheduledShoppingItem
{
    [Required]
    public int Id { get; set; }

    [Required]
    public DateTime Day { get; set; }

    [Required]
    public bool Bought { get; set; }

    [Required]
    public ShoppingItem ShoppingItem { get; set; }
}
