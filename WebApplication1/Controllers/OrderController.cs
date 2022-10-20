using BethanysPieShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly ShoppingCart shoppingCart;
        private readonly ILogger<OrderController> logger;

        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart, ILogger<OrderController> logger)
        {
            this.orderRepository = orderRepository;
            this.shoppingCart = shoppingCart;
            this.logger = logger;
        }
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = shoppingCart.GetShoppingCartItems();

            shoppingCart.ShoppingCartItems = items;

            if(shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some pies first");
            }

            if (ModelState.IsValid)
            {
                logger.LogInformation("{FirstName} - Has checkout the orders", order.FirstName);
                orderRepository.CreateOrder(order);
                shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order. You'll soon enjoy your delicious pies!";

            return View();
        }
    }
}
