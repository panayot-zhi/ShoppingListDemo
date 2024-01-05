using System.ComponentModel.DataAnnotations;

namespace ShoppingListDemo.Data;

public class ShoppingCategory
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public ICollection<ShoppingItem>? ShoppingItems { get; set; }
}
