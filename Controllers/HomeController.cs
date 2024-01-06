using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingListDemo.Data;
using ShoppingListDemo.Models;
using System.Diagnostics;
using System.Security.Cryptography;

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

        // GET: ScheduledShoppingItems
        public async Task<IActionResult> Index([FromQuery] DateTime? at)
        {
            IQueryable<ScheduledShoppingItem> scheduledShoppingItemsQuery = _context.ScheduledShoppingItems;
            IQueryable<ShoppingItem> shoppingItemsQuery = _context.ShoppingItems
                .Include(s => s.ShoppingCategory);

            if (at.HasValue)
            {
                scheduledShoppingItemsQuery = scheduledShoppingItemsQuery
                    .Include(x => x.ShoppingItem)
                        .ThenInclude(x => x.ShoppingCategory)
                    .Where(x => x.Day == at.Value.Date);
            }

            var scheduledShoppingItems = await scheduledShoppingItemsQuery.ToListAsync();
            var shoppingItems = await shoppingItemsQuery.ToListAsync();
            var viewModel = new ShoppingListViewModel()
            {
                CurrentDate = at ?? DateTime.Now,
                CurrentShoppingItems = scheduledShoppingItems,
                AllShoppingItems = shoppingItems
            };

            return View(viewModel);
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
