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

    [Required]
    [Column(TypeName = "decimal(8, 2)")]
    public decimal Price { get; set; }

    [Required]
    [MaxLength(65)]
    public string Currency { get; set; }

    [ForeignKey("ShoppingCategory")]
    public int ShoppingCategoryId { get; set; }

    public ShoppingCategory ShoppingCategory { get; set; }

    public ICollection<ShoppingSchedule> ShoppingSchedules { get; set; }

}
