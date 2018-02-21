using Bangazon.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.OrderViewModels
{
    /*
        Author:     Krys Mathis
        Purpose:    To support the view of the available payment types for the active user
                    In the constructor, the program instantiates an empty list of payment
                    types in order to handle situations where the user has no valid payment types
    */
    public class AvailablePaymentTypesViewModel
    {

        public List<SelectListItem> PaymentTypeId { get; set; }
        public Order Order { get; set; }

        public AvailablePaymentTypesViewModel(ApplicationDbContext context, ApplicationUser user, Order order)
        {
            Order = order;
            this.PaymentTypeId = context.PaymentType.Where(p => p.User == user)
                                    .AsEnumerable()
                                    .Select(li => new SelectListItem
                                    {
                                        Text = li.Description,
                                        Value = li.PaymentTypeId.ToString()
                                    }).ToList();

            this.PaymentTypeId.Insert(0, new SelectListItem
            {
                Text = "Choose payment type...",
                Value = "0"
            });

        }
    }
}
