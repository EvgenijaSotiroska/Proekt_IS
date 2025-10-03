using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CoffeeEShop.Domain;
using CoffeeEShop.Service.Interface;
using CoffeeEShop.Service.Implementation;

namespace CoffeeEShop.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IShopService _shopService;

    public HomeController(ILogger<HomeController> logger, IShopService shopService)
    {
        _logger = logger;
        _shopService = shopService;
    }

    public IActionResult Index()
    {
        var allShops = _shopService.GetAll();  
        var featuredShops = allShops.Take(3);
        return View(featuredShops);
    
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
