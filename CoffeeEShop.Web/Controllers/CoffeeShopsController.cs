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

namespace CoffeeEShop.Web.Controllers
{
    public class CoffeeShopsController : Controller
    {
        private readonly IShopService _coffeeShopService;

        public CoffeeShopsController(IShopService coffeeShopService)
        {
            _coffeeShopService = coffeeShopService;
        }

        // GET: CoffeeShops
        public IActionResult Index()
        {
            return View(_coffeeShopService.GetAll());
        }

        // GET: CoffeeShops/Details/5
        public IActionResult Details(Guid id)
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
        public IActionResult Create([Bind("Name,Address,Rating,Id")] Shop coffeeShop)
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
        public IActionResult Edit(Guid id, [Bind("Name,Address,Rating,Id")] Shop coffeeShop)
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

    }
}
