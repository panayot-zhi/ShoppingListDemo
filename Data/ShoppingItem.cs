using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingListDemo.Data;

[Display(Name = "Продукт")]
public class ShoppingItem
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    [Display(Name = "Име")]
    public string Name { get; set; }

    [Display(Name = "Категория")]
    [ForeignKey("ShoppingCategory")]
    public int ShoppingCategoryId { get; set; }

    [Display(Name = "Категория")]
    public ShoppingCategory? ShoppingCategory { get; set; }

}
