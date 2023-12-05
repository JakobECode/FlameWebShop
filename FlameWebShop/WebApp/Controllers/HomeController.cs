using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _productService;

        public HomeController(ProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var featuredProducts = await _productService.GetByTagAsync("Featured");
            var newProducts = await _productService.GetByTagAsync("New");
            var popularProducts = await _productService.GetByTagAsync("Popular");

            HomeViewModel model = new HomeViewModel()
            {
                Featured = featuredProducts.ToList(),
                New = newProducts.ToList(),
                Popular = popularProducts.ToList()
            };

            return View();
        }
    }
}
