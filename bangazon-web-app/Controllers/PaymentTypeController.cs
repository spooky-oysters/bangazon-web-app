﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bangazon.Data;
using Bangazon.Models;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;

namespace Bangazon.Controllers
{
    public class PaymentTypeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public PaymentTypeController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: PaymentType
        public async Task<IActionResult> Index()
        {
            // Grabs the current user and only displays the current users payment types
            var user = await GetCurrentUserAsync();
            return View(await _context.PaymentType.Where(p => p.User == user).ToListAsync());
        }

        // GET: PaymentType/Create
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {

            // Get current user
            var user = await GetCurrentUserAsync();

            return View();
        }

        // POST: PaymentType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentType paymentType)
        {
            // Removes validation for the user
            ModelState.Remove("User");

            // Instantiates the current user to add to the paymentType
            var user = await GetCurrentUserAsync();
            paymentType.User = user;

            if (ModelState.IsValid)
            {
                // Adds the paymentType to the database
                _context.Add(paymentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(paymentType);
        }

        // GET: PaymentType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _context.PaymentType
                .SingleOrDefaultAsync(m => m.PaymentTypeId == id);
            if (paymentType == null)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // POST: PaymentType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentType = await _context.PaymentType.SingleOrDefaultAsync(m => m.PaymentTypeId == id);
            _context.PaymentType.Remove(paymentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentTypeExists(int id)
        {
            return _context.PaymentType.Any(e => e.PaymentTypeId == id);
        }
    }
}
