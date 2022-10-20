using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Web;

namespace BethanysPieShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ILogger<HomeController> logger;

        public HomeController(IPieRepository pieRepository, ILogger<HomeController> logger)
        {
            _pieRepository = pieRepository;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            logger.LogInformation("Home page is being accessed");
            var homeViewModel = new HomeViewModel
            {
                PiesOfTheWeek = _pieRepository.PiesOfTheWeek
            };
            return View(homeViewModel);
        }
    }
}
