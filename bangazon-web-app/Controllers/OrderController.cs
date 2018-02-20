using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bangazon.Data;
using Bangazon.Models;
using Bangazon.Models.OrderViewModels;
using Microsoft.AspNetCore.Identity;

namespace Bangazon.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private Order order;

        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        
        // GET: Order
        public async Task<IActionResult> Index()
        {
            ShoppingCartViewModel model = new ShoppingCartViewModel();
            ApplicationUser user = await GetCurrentUserAsync();

            // loop through the correct order and display it
            // each line will be a shopping cart view model
            // will be appended to the model
            
            // if this is null then there are no active orders
            var userOrders = _context.Order.Where(o => o.User == user && o.CompletedDate == null).SingleOrDefault();
            order = userOrders;
            
            // get the line items - don't need the order just the products
            var userLineItems = _context.LineItem.Where(l => l.OrderId == userOrders.OrderId);
            
            // sum of all the line items into a collection of anonymous objects
            var lineItemQuantity = from product in userLineItems
                          group product by product.ProductId into grouped
                          select new { grouped.Key, quantity = grouped.Count() };

            try
            {
                model.OrderId = userOrders.OrderId;
                model.ShoppingCart = (from l in lineItemQuantity
                                      join product in _context.Product on l.Key equals product.ProductId
                                      select new ShoppingCartLineItemViewModel
                                      {
                                          ProductName = product.Title,
                                          Units = l.quantity,
                                          Total = l.quantity * product.Price
                                      }).ToList();
            }

            catch { }
            // Capture the fields for the ShoppingCartViewModel
            
            return View(model);
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Order/Complete - edit the existing form
        public async Task<IActionResult> Complete (int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            ApplicationUser user = await GetCurrentUserAsync();
            AvailablePaymentTypesViewModel paymentTypes = new AvailablePaymentTypesViewModel(_context, user, order.OrderId);

            return View(paymentTypes);
        }

       
        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind("OrderId,PaymentTypeId,CompletedDate,CreatedDate")] Order order
        public async Task<IActionResult> CompleteOrder ()
        {
            return RedirectToAction("Index");
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,PaymentTypeId,CompletedDate,CreatedDate")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

             return RedirectToAction("Delete");
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(m => m.OrderId == id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
