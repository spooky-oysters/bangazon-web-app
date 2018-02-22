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
using System.ComponentModel.DataAnnotations;

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


        /*
            Author:     Krys Mathis
            Purpose:    Display product details page, which includes a calculation for the 
                        available quantity for the items
            ViewModel:  ProductDetailViewModel
        */
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

            ProductDetailViewModel model = new ProductDetailViewModel();
            model.Title = product.Title;
            model.Description = product.Description;
            model.Price = product.Price;
            model.ProductId = product.ProductId;


            int initialQuantity = product.Quantity;

            var lineItemsWithProduct = _context.LineItem.Where(l => l.ProductId == product.ProductId);
            var onlyLineItemsOnClosedOrders = from l in lineItemsWithProduct
                                              join o in _context.Order on l.OrderId equals o.OrderId
                                              where o.PaymentTypeId != null
                                              select new { l.ProductId };

            model.AvailableQuantity = initialQuantity - onlyLineItemsOnClosedOrders.Count();

            return View(model);
        }

        /*
            Author:     Krys Mathis
            Purpose:    To add a product to an order. In cases where the current user does not have
                        an active order it will create an open order.
            Parameters: ProductId
        */
    
        [Authorize]
        public async Task<IActionResult> AddProductToCart(int? id) {

            ApplicationUser user = await GetCurrentUserAsync();

            Order order = GetOpenOrderForUser(user);

            if (order == null)
            {
                order = CreateNewOrderForUser(user);
            }

            LineItem lineItem = new LineItem() { OrderId = order.OrderId, ProductId = Convert.ToInt32(id) };

            _context.LineItem.Add(lineItem);
            await _context.SaveChangesAsync();

            // TODO: have it trigger an alert to the user that this occurred?

            return RedirectToAction("Details",new { id = id });
        }

        /*
            Author:     Krys Mathis
            Purpose:    Check if current user has an open order
            Parameter:  User
            Return:     bool
        */
        public Order GetOpenOrderForUser(ApplicationUser user) {
            
            return _context.Order.Where(o => o.User == user && o.PaymentTypeId == null).SingleOrDefault();
   
        }
        public Order CreateNewOrderForUser(ApplicationUser user) {
            // create new orer
            Order order = new Order() { User = user, CreatedDate = DateTime.Now };
           
            _context.Order.Add(order);
            _context.SaveChangesAsync();

            return order;
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
         It also now works with searching by city name. 
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
        
    }
}
