using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.OrderViewModels
{
    public class ShoppingCartLineItemViewModel
    {
        public string ProductName { get; set; }
        public int Units { get; set; }
        public double Total { get; set; }
        
    }
}
