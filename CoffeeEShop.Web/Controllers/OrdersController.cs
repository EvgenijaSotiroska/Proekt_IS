using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoffeeEShop.Domain.DomainModels;
using CoffeeEShop.Repository;
using CoffeeEShop.Domain.Identity;
using CoffeeEShop.Service.Interface;
using Microsoft.AspNetCore.Identity;
using CoffeeEShop.Repository.Interface;
using CoffeeEShop.Domain.DTO;

namespace CoffeeEShop.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<SystemUser> _userManager;
        private readonly IRepository<ProductInOrder> _productInOrderRepository;

        public OrdersController(IOrderService orderService, UserManager<SystemUser> userManager, IRepository<ProductInOrder> productInOrderRepository)
        {
            _orderService = orderService;
            _userManager = userManager;
            _productInOrderRepository = productInOrderRepository;
        }

        public async Task<IActionResult> MyOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var order = _orderService.GetByUserId(Guid.Parse(user.Id));

            if (order == null) return View("EmptyOrder");

            var productsInOrder = _productInOrderRepository.GetAll(selector: x => x,
                                                              predicate: pio => pio.OrderId == order.Id,
                                                              include: query => query.Include(pio => pio.Product)).ToList();

            var viewModel = productsInOrder.Select(pio => new OrderDTO
            {
                SelectedProductId = pio.ProductId,
                SelectedProductName = pio.Product!.Name,
                Quantity = pio.Quantity,
                Price = pio.Product.Price
            }).ToList();

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var order = _orderService.GetByUserId(Guid.Parse(user.Id));
            if (order != null)
            {
                _orderService.CompleteOrder(order);
            }
            return RedirectToAction("Index", "Products");
        }
        }
}
