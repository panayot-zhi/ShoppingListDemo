using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingListDemo.Data;

namespace ShoppingListDemo.Controllers;

public class ShoppingItemsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ShoppingItemsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: ShoppingItems
    public async Task<IActionResult> Index(int? shoppingCategoryId)
    {
        IQueryable<ShoppingItem> shoppingItemsQuery = _context.ShoppingItems
            .Include(s => s.ShoppingCategory);

        if (shoppingCategoryId.HasValue)
        {
            shoppingItemsQuery = shoppingItemsQuery.Where(x => x.ShoppingCategoryId == shoppingCategoryId.Value);
        }

        var shoppingItems = await shoppingItemsQuery.ToListAsync();
        return View(shoppingItems);
    }

    // GET: ShoppingItems/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var shoppingItem = await _context.ShoppingItems
            .Include(s => s.ShoppingCategory)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (shoppingItem == null)
        {
            return NotFound();
        }

        return View(shoppingItem);
    }

    // GET: ShoppingItems/Create
    public IActionResult Create(int? shoppingCategoryId)
    {
        ViewData["ShoppingCategoryId"] = new SelectList(_context.ShoppingCategories, 
            "Id", "Name", shoppingCategoryId);
        return View();
    }

    // POST: ShoppingItems/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,ShoppingCategoryId")] ShoppingItem shoppingItem)
    {
        if (!ModelState.IsValid)
        {
            ViewData["ShoppingCategoryId"] = new SelectList(_context.ShoppingCategories,
                "Id", "Name", shoppingItem.ShoppingCategoryId);
            return View(shoppingItem);
        }

        _context.Add(shoppingItem);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: ShoppingItems/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var shoppingItem = await _context.ShoppingItems.FindAsync(id);
        if (shoppingItem == null)
        {
            return NotFound();
        }

        ViewData["ShoppingCategoryId"] = new SelectList(_context.ShoppingCategories, 
            "Id", "Name", shoppingItem.ShoppingCategoryId);
        return View(shoppingItem);
    }

    // POST: ShoppingItems/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ShoppingCategoryId")] ShoppingItem shoppingItem)
    {
        if (id != shoppingItem.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            ViewData["ShoppingCategoryId"] = new SelectList(_context.ShoppingCategories, 
                "Id", "Name", shoppingItem.ShoppingCategoryId);
            return View(shoppingItem);
        }

        try
        {
            _context.Update(shoppingItem);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ShoppingItemExists(shoppingItem.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: ShoppingItems/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var shoppingItem = await _context.ShoppingItems
            .Include(s => s.ShoppingCategory)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (shoppingItem == null)
        {
            return NotFound();
        }

        return View(shoppingItem);
    }

    // POST: ShoppingItems/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var shoppingItem = await _context.ShoppingItems.FindAsync(id);
        if (shoppingItem != null)
        {
            _context.ShoppingItems.Remove(shoppingItem);
        }
            
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ShoppingItemExists(int id)
    {
        return _context.ShoppingItems.Any(e => e.Id == id);
    }
}