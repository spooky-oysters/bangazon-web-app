using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bangazon.Controllers
{
    public class AccountSettingsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AccountSettingsController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context
            )
        {
            _context = context;
            _userManager = userManager;
        }

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IActionResult> Edit()
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var user = await GetCurrentUserAsync();
            AccountSettingsViewModel model = new AccountSettingsViewModel();
            model.FirstName
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}