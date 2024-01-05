using System.ComponentModel.DataAnnotations;

namespace ShoppingListDemo.Data;

public class ShoppingSchedule
{
    [Required]
    public int Id { get; set; }

    [Required]
    public DateTime Day { get; set; }

    public ICollection<ShoppingItem> ShoppingItems { get; set; }
}
