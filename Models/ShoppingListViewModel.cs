using ShoppingListDemo.Data;

namespace ShoppingListDemo.Models;

public class ShoppingListViewModel
{
    public DateTime CurrentDate { get; set; }

    public List<ShoppingItem> AllShoppingItems { get; set; } = new();

    public List<ScheduledShoppingItem> CurrentShoppingItems { get; set; } = new();
}
