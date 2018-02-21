using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // Holds the success message on successfully updating user profile
        [TempData]
        public string SuccessMessage { get; set; }

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IActionResult> Edit()
        {
            // Instantiates the current user
            var user = await GetCurrentUserAsync();

            // Instantiates the view model 
            AccountSettingsViewModel model = new AccountSettingsViewModel();

            // Adds the properties of the user to the view model for display
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Address = user.StreetAddress;
            model.PhoneNumber = user.PhoneNumber;

            if (user == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // Author: Dre Randaci
        // Updates the current users last name, phone number, and street address
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AccountSettingsViewModel model)
        {
            // Instantiates the current user
            var user = await GetCurrentUserAsync();

            // Adds the updated properties
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.StreetAddress = model.Address;

            if (ModelState.IsValid)
            {
                try
                {
                    // Updates the user in the database
                    _context.Update(user);                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (user == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Adds a message on successful profile update
                TempData["SuccessMessage"] = "Your account has been updated";
            }          
            return RedirectToAction("Edit");
        }        
    }
}