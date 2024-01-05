using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingListDemo.Data;

public class ShoppingItem
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [ForeignKey("ShoppingCategory")]
    public int ShoppingCategoryId { get; set; }

    public ShoppingCategory? ShoppingCategory { get; set; }

    public ICollection<ScheduledShoppingItem>? ShoppingSchedules { get; set; }

}
