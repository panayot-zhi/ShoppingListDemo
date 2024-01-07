using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingListDemo.Data;

namespace ShoppingListDemo.Controllers;

public class ShoppingCategoriesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ShoppingCategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: ShoppingCategories
    public async Task<IActionResult> Index()
    {
        var shoppingCategories = await _context.ShoppingCategories
            .OrderBy(x => x.Order)
            .ToListAsync();
        return View(shoppingCategories);
    }

    // GET: ShoppingCategories/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var shoppingCategory = await _context.ShoppingCategories
            .Include(x => x.ShoppingItems)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (shoppingCategory == null)
        {
            return NotFound();
        }

        return View(shoppingCategory);
    }

    // GET: ShoppingCategories/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ShoppingCategories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] ShoppingCategory shoppingCategory)
    {
        if (!ModelState.IsValid)
        {
            return View(shoppingCategory);
        }

        var order = 10;
        if (_context.ShoppingCategories.Any())
        {
            order = _context.ShoppingCategories
                .Max(x => x.Order) + 10;
        }

        shoppingCategory.Order = order;

        _context.Add(shoppingCategory);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

    // GET: ShoppingCategories/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var shoppingCategory = await _context.ShoppingCategories.FindAsync(id);
        if (shoppingCategory == null)
        {
            return NotFound();
        }

        return View(shoppingCategory);
    }

    // POST: ShoppingCategories/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Order")] ShoppingCategory shoppingCategory)
    {
        if (id != shoppingCategory.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(shoppingCategory);
        }

        try
        {
            _context.Update(shoppingCategory);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ShoppingCategoryExists(shoppingCategory.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: ShoppingCategories/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var shoppingCategory = await _context.ShoppingCategories
            .Include(x => x.ShoppingItems)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (shoppingCategory == null)
        {
            return NotFound();
        }

        return View(shoppingCategory);
    }

    // POST: ShoppingCategories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var shoppingCategory = await _context.ShoppingCategories.FindAsync(id);
        if (shoppingCategory != null)
        {
            _context.ShoppingCategories.Remove(shoppingCategory);
        }
            
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ShoppingCategoryExists(int id)
    {
        return _context.ShoppingCategories.Any(e => e.Id == id);
    }
}