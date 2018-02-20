using Bangazon.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models
{
    public class AccountSettingsViewModel
    {
        public ApplicationUser User { get; set; }

        public ICollection<PaymentType> PaymentOptions { get; set; }
        public ICollection<Order> OrderHistory { get; set; }

        public AccountSettingsViewModel(ApplicationDbContext ctx)
        {

        }
    }
}
