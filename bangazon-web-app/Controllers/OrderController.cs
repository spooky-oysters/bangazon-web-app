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

            // If the user is not authenticated do not show order
            if (user == null) {
                return RedirectToAction("Index", "Home");
            }
            // loop through the correct order and display it
            // each line will be a shopping cart view model
            // will be appended to the model
            
            // if this is null then there are no active orders
            var userOrders = _context.Order.Where(o => o.User == user && o.CompletedDate == null).SingleOrDefault();

            
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


        // GET: Order/Complete - edit the existing form
        // This action sends the user to
        public async Task<IActionResult> Complete (int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            // Convert the id to an int
            id = Convert.ToInt32(id);

            // Get the open order and validate that one is returned
            Order openOrder = _context.Order.Where(o => o.OrderId == id).SingleOrDefault();
            
            if (openOrder == null)
            {
                return NotFound();
            }

            // get the current user
            ApplicationUser user = await GetCurrentUserAsync();
            CompleteOrderViewModel paymentTypes = new CompleteOrderViewModel(_context, user, openOrder);

            return View(paymentTypes);
        }


        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind("OrderId,PaymentTypeId,CompletedDate,CreatedDate")] Order order
        public async Task<IActionResult> CompleteOrder(int id, [Bind("OrderId,PaymentTypeId,CompletedDate,CreatedDate")] Order order)
        {
            {
                if (order == null)
                {
                    return NotFound();
                }

                ModelState.Remove("order.User");
                ModelState.Remove("order.LineItem");

                // assign the date time
                order.CompletedDate = DateTime.Now;

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
