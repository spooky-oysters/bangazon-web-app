using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Models;
using Bangazon.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System;
using Bangazon.Models.ProductViewModels;

// Author: Kimberly Bird
// Controller for products

namespace Bangazon.Controllers
{
    public class ProductsController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _context = ctx;
            _userManager = userManager;
        }

        // Retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Product.Include(p => p.ProductType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductType)
                .SingleOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            ProductCreateViewModel model = new ProductCreateViewModel(_context);

            // Get current user
            var user = await GetCurrentUserAsync();
            return View(model);
<<<<<<< HEAD


=======
>>>>>>> master
        }

        // POST: Products/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            // Remove the user from the model validation because it is
            // not information posted in the form
            ModelState.Remove("product.User");

            if (ModelState.IsValid)
            {
                /*
                    If all other properties validation, then grab the 
                    currently authenticated user and assign it to the 
                    product before adding it to the db _context
                */
                var user = await GetCurrentUserAsync();
                product.User = user;

                _context.Add(product);

                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = product.ProductId });
            }

            ProductCreateViewModel model = new ProductCreateViewModel(_context);
            return View(model);
        }

        /*
         Author: John Dulaney
         Purpose: this method is used by the search bar in the navbar. 
         It pulls the products table and sorts through it looking for products that contain the query
             */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchForProduct(string search)
        {
            //return as 404 if search is null or not found in db
            if (search == null)
            {
                return NotFound();                                                      
            }
            //find any product that contain the searched value 
            var product = await _context.Product
                .Include(p => p.ProductType)
                .Where(m => m.Title.Contains(search) || m.City.Contains(search))
                .ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchForProductByCity(string search)
        {
            //return as 404 if search is null or not found in db
            if (search == null)
            {
                return NotFound();
            }
            //find any product that contain the searched value 
            var product = await _context.Product
                .Include(p => p.ProductType)
                .Where(m => m.City.Contains(search))
                .ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
