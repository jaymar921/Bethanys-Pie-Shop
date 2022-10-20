using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly ILogger<ShoppingCartController> logger;

        public ShoppingCartController(IPieRepository pieRepository, ShoppingCart shoppingCart, ILogger<ShoppingCartController> logger)
        {
            _pieRepository = pieRepository;
            _shoppingCart = shoppingCart;
            this.logger = logger;
        }
        
        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int pieId)
        {
            var selectedPie = _pieRepository.AllPies.FirstOrDefault(p => p.PieId == pieId);

            if(selectedPie != null)
            {
                logger.LogInformation("Someone added {PieName} to cart", selectedPie.Name);
                _shoppingCart.AddToCart(selectedPie, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        {
            var selectedPie = _pieRepository.AllPies.FirstOrDefault(p => p.PieId == pieId);

            if(selectedPie != null)
            {
                _shoppingCart.RemoveFromCart(selectedPie);
            }
            return RedirectToAction("Index");
        }
    }
}
