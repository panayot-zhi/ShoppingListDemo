using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingListDemo.Data;
using ShoppingListDemo.Models;
using System.Diagnostics;

namespace ShoppingListDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index([FromQuery] DateTime? at)
        {
            at ??= DateTime.Now;

            // NOTE: If we are retrieving the whole table at least limit the query!
            var allScheduledShoppingItems = await _context.ScheduledShoppingItems
                .Where(x => at.Value.AddMonths(-2) <= x.Day && x.Day <= at.Value.AddMonths(2))
                .OrderBy(x => x.Day)
                .ToListAsync();

            var scheduledShoppingItemsCountPerDay = allScheduledShoppingItems
                .GroupBy(x => x.Day)
                .ToDictionary(x => x.Key.ToString("yyyy-MM-dd"), x => x.Count());

            var scheduledShoppingItems = await _context.ScheduledShoppingItems
                .Include(x => x.ShoppingItem)
                    .ThenInclude(x => x!.ShoppingCategory)
                .Where(x => x.Day == at.Value.Date)
                .ToListAsync();

            var shoppingItems = await _context.ShoppingItems
                .Include(s => s.ShoppingCategory)
                .ToListAsync();

            var viewModel = new ShoppingListViewModel()
            {
                CurrentDate = at.Value,
                CurrentShoppingItems = scheduledShoppingItems,
                ScheduledShoppingItemsCountPerDay = scheduledShoppingItemsCountPerDay,
                AllShoppingItems = shoppingItems
            };

            return View(viewModel);
        }

        [HttpPost("addItem")]
        public async Task<IActionResult> AddItem([FromBody] AddItemModel model)
        {
            var shoppingItem = await _context.ShoppingItems.FindAsync(model.ShoppingItemId);
            if (shoppingItem is null)
            {
                return NotFound();
            }

            var scheduledShoppingItem = new ScheduledShoppingItem()
            {
                ShoppingItemId = shoppingItem.Id,
                Day = model.Day
            };

            var entry = await _context.ScheduledShoppingItems.AddAsync(scheduledShoppingItem);
            
            await _context.SaveChangesAsync();

            return Ok(entry.Entity);
        }

        [HttpPost("removeItem/{scheduledShoppingItemId}")]
        public async Task<IActionResult> RemoveItem(int scheduledShoppingItemId)
        {
            var scheduledShoppingItem = await _context.ScheduledShoppingItems.FindAsync(scheduledShoppingItemId);
            if (scheduledShoppingItem is null)
            {
                return NotFound();
            }

            _context.ScheduledShoppingItems.Remove(scheduledShoppingItem);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("toggleBought/{scheduledShoppingItemId}")]
        public async Task<IActionResult> ToggleBought(int scheduledShoppingItemId)
        {
            var scheduledShoppingItem = await _context.ScheduledShoppingItems.FindAsync(scheduledShoppingItemId);
            if (scheduledShoppingItem is null)
            {
                return NotFound();
            }

            _context.Attach(scheduledShoppingItem);

            scheduledShoppingItem.Bought = !scheduledShoppingItem.Bought;

            await _context.SaveChangesAsync();

            return Ok(scheduledShoppingItem);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = HttpContext.TraceIdentifier
            };

            if (Activity.Current is not null)
            {
                errorViewModel.RequestId = Activity.Current.Id;
            }

            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature != null)
            {
                var error = exceptionHandlerPathFeature.Error;
                errorViewModel.Path = exceptionHandlerPathFeature.Path;
                errorViewModel.ErrorMessage = error.Message;
                errorViewModel.StackTrace = error.StackTrace;
            }

            _logger.LogError("{RequestId} ({Path}): {ErrorMessage}",
                errorViewModel.RequestId, errorViewModel.Path, errorViewModel.ErrorMessage);

            return View(errorViewModel);
        }
    }
}
