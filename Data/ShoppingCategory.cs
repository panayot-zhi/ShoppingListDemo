using System.ComponentModel.DataAnnotations;

namespace ShoppingListDemo.Data;

[Display(Name = "Категория")]
public class ShoppingCategory
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    [Display(Name = "Име")]
    public string Name { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    [Display(Name = "Приоритет")]
    public int Order { get; set; }

    [Display(Name = "Продукти")]
    public ICollection<ShoppingItem> ShoppingItems { get; set; } = new List<ShoppingItem>();
}
