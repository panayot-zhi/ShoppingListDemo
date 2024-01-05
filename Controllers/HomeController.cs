using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShoppingListDemo.Models;
using System.Diagnostics;
using System.Security.Cryptography;

namespace ShoppingListDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
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
