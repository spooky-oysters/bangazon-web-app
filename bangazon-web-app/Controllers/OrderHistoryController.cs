using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Data;
using Bangazon.Models;
using Bangazon.Models.OrderHistoryViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Controllers
{
    public class OrderHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderHistoryController(
            ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: OrderHistory
        public async Task<IActionResult> Index()
        {
            // Instantiate the ViewModel
            OrderHistoryViewModel model = new OrderHistoryViewModel();

            // Grab the currently logged in user
            var user = await GetCurrentUserAsync();

            // Queries for orders that have been completed and includes all the payment types associated with the orders
            model.Orders = await _context
                .Order
                .Include("PaymentType")
                .Where(o => o.User == user && o.PaymentTypeId != null)
                .ToListAsync();

            return View(model);
        }

        // GET: OrderHistory/Details/5
        public async Task<IActionResult> Details(int id)
        {
            OrderHistoryDetailViewModel model = new OrderHistoryDetailViewModel();

            model.Order = await _context
                 .Order
                 .SingleOrDefaultAsync(o => o.OrderId == id);

            model.Products = await _context
                 .LineItem
                 .Include("Product")
                 .Where(l => l.OrderId == id)
                 .ToListAsync();

            if (model.Products == null)
            {
                return NotFound();
            }

            return View(model);
        }

        
    }
}