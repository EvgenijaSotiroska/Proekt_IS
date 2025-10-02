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
using CoffeeEShop.Service.Interface;
using CoffeeEShop.Repository.Interface;
using CoffeeEShop.Domain.ViewModels;

namespace CoffeeEShop.Web.Controllers
{
    public class CoffeeShopsController : Controller
    {
        private readonly IShopService _coffeeShopService;
        private readonly IProductService _productService;
        private readonly IRepository<ProductInCoffeeShop> _productInCoffeeShopRepository;   

        public CoffeeShopsController(IShopService coffeeShopService, IRepository<ProductInCoffeeShop> productInCoffeeShopRepository, IProductService productService)
        {
            _coffeeShopService = coffeeShopService;
            _productInCoffeeShopRepository = productInCoffeeShopRepository;
            _productService = productService;
        }

        // GET: CoffeeShops
        public IActionResult Index()
        {
            return View(_coffeeShopService.GetAll());
        }

        // GET: CoffeeShops/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var coffeeShop = _coffeeShopService.GetById(id);
            if (coffeeShop == null)
            {
                return NotFound();
            }

            // Get only products that belong to this shop
            var products = _productInCoffeeShopRepository.GetAll(
                selector: x => x.Product,
                predicate: p => p.CoffeeShopId == id
            ).ToList();

            var model = new ShopDetailsViewModel
            {
                Shop = coffeeShop,
                Products = products
            };

            // 🔹 Add all available products for dropdown
            ViewBag.AllProducts = _productService.GetAll();

            return View(model);
        }

        // GET: CoffeeShops/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CoffeeShops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Address,Rating,Image,Id")] Shop coffeeShop)
        {
            if (ModelState.IsValid)
            {
                coffeeShop.Id = Guid.NewGuid();
                _coffeeShopService.Insert(coffeeShop);
                return RedirectToAction(nameof(Index));
            }
            return View(coffeeShop);
        }

        // GET: CoffeeShops/Edit/5
        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coffeeShop = _coffeeShopService.GetById(id);
            if (coffeeShop == null)
            {
                return NotFound();
            }
            return View(coffeeShop);
        }

        // POST: CoffeeShops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Address,Rating,Image,Id")] Shop coffeeShop)
        {
            if (id != coffeeShop.Id)
            {
                return NotFound();
            }

            _coffeeShopService.Update(coffeeShop);
            return RedirectToAction(nameof(Index));
        }

        // GET: CoffeeShops/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coffeeShop = _coffeeShopService.GetById(id);
            if (coffeeShop == null)
            {
                return NotFound();
            }

            return View(coffeeShop);
        }

        // POST: CoffeeShops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            _coffeeShopService.DeleteById(id);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult AddProductToShop(Guid shopId)
        {
            // Load all products
            var products = _productService.GetAll();

            ViewBag.ShopId = shopId; // pass shop id
            return View(products);
        }

        [HttpPost]
        public IActionResult AddProductToShop(Guid shopId, Guid productId)
        {

            var exists = _productInCoffeeShopRepository
                .GetAll(
                    selector: x => x,   // we want the whole entity
                    predicate: p => p.ProductId == productId && p.CoffeeShopId == shopId
                )
                .Any();

            if (exists)
            {
                TempData["Error"] = "This product is already in the menu.";
                return RedirectToAction("Details", new { id = shopId });
            }

            var entity = new ProductInCoffeeShop
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                CoffeeShopId = shopId
            };

            _productInCoffeeShopRepository.Insert(entity);

            TempData["Success"] = "Product successfully added to the menu.";
            return RedirectToAction("Details", new { id = shopId });
        }




    }
}
