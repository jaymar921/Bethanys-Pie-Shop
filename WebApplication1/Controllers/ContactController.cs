using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Controllers
{
    public class ContactController : Controller
    {
        private readonly IConfiguration configuration;

        public ContactController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            // binding configuration
            var feature = new Feature();
            configuration.Bind("Features", feature);
            return View(feature);
        }

    }
}
