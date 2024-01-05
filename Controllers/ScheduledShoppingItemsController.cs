using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingListDemo.Data;

namespace ShoppingListDemo.Controllers
{
    public class ScheduledShoppingItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScheduledShoppingItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ScheduledShoppingItems
        public async Task<IActionResult> Index([FromQuery] DateTime? at)
        {
            IQueryable<ScheduledShoppingItem> scheduledShoppingItemsQuery = _context.ScheduledShoppingItems;

            if (at.HasValue)
            {
                scheduledShoppingItemsQuery = scheduledShoppingItemsQuery.Where(x => x.Day == at.Value.Date);
            }

            var scheduledShoppingItems = await scheduledShoppingItemsQuery.ToListAsync();
            return View(scheduledShoppingItems);
        }

        // GET: ScheduledShoppingItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduledShoppingItem = await _context.ScheduledShoppingItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scheduledShoppingItem == null)
            {
                return NotFound();
            }

            return View(scheduledShoppingItem);
        }

        // GET: ScheduledShoppingItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ScheduledShoppingItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Day,Bought")] ScheduledShoppingItem scheduledShoppingItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scheduledShoppingItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scheduledShoppingItem);
        }

        // GET: ScheduledShoppingItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ScheduledShoppingItems == null)
            {
                return NotFound();
            }

            var scheduledShoppingItem = await _context.ScheduledShoppingItems.FindAsync(id);
            if (scheduledShoppingItem == null)
            {
                return NotFound();
            }
            return View(scheduledShoppingItem);
        }

        // POST: ScheduledShoppingItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Day,Bought")] ScheduledShoppingItem scheduledShoppingItem)
        {
            if (id != scheduledShoppingItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scheduledShoppingItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduledShoppingItemExists(scheduledShoppingItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(scheduledShoppingItem);
        }

        // GET: ScheduledShoppingItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ScheduledShoppingItems == null)
            {
                return NotFound();
            }

            var scheduledShoppingItem = await _context.ScheduledShoppingItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scheduledShoppingItem == null)
            {
                return NotFound();
            }

            return View(scheduledShoppingItem);
        }

        // POST: ScheduledShoppingItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ScheduledShoppingItems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ScheduledShoppingItems'  is null.");
            }
            var scheduledShoppingItem = await _context.ScheduledShoppingItems.FindAsync(id);
            if (scheduledShoppingItem != null)
            {
                _context.ScheduledShoppingItems.Remove(scheduledShoppingItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduledShoppingItemExists(int id)
        {
          return (_context.ScheduledShoppingItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
