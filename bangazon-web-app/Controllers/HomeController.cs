using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bangazon.Models;
using Microsoft.AspNetCore.Identity;
using Bangazon.Data;
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Controllers
{
    public class HomeController : Controller
    {
        /*
         Author(s): John Dulaney
         Purpose: This controller injects data into our index page.
         HTTPGET method for grabbing 20 products and displaying them. 
        */
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _context = ctx;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var home = await TopTwentyProducts();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TopTwentyProducts()
        {
            // Dig into the product table, pull the date created key and order it descending, 20 selections max
            var product = await _context.Product
                .Include(p => p.ProductType)
                .OrderByDescending(d => d.DateCreated)
                .Take(20)
                .ToListAsync();

            return View(product);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}