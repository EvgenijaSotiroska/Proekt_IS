using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoffeeEShop.Domain.DomainModels;
using CoffeeEShop.Repository;
using CoffeeEShop.Service.Implementation;

namespace CoffeeEShop.Web.Controllers
{
    public class DetailedProductsController : Controller
    {
        private readonly DetailedProductService _detailedProductService;

        public DetailedProductsController(DetailedProductService detailedProductService)
        {
            _detailedProductService = detailedProductService;
        }

        public async Task<IActionResult> AllCoffees()
        {
            var allCoffees = await _detailedProductService.GetAllCoffeesAsync();
            return View(allCoffees);
        }
    }
}
