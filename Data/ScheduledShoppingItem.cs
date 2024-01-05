using System.ComponentModel.DataAnnotations;

namespace ShoppingListDemo.Data;

[Display(Name = "Продукт")]
public class ScheduledShoppingItem
{
    [Required]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Ден")]
    public DateTime Day { get; set; }

    [Required]
    [Display(Name = "Купен")]
    public bool Bought { get; set; }

    [Required]
    [Display(Name = "Продукт")]
    public ShoppingItem ShoppingItem { get; set; }
}
