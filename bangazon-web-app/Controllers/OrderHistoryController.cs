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
            OrderHistoryViewModel model = new OrderHistoryViewModel();

            var user = await GetCurrentUserAsync();

            model.Orders = _context.Order.Include("PaymentType")
                .Where(o => o.User == user && o.PaymentTypeId != null).ToList();

            return View(model);
        }

        // GET: OrderHistory/Details/5
        public async Task<IActionResult> Details(int id)
        {

            return View();
        }

        
    }
}