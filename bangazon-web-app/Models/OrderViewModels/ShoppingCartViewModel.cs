﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.OrderViewModels
{
    public class ShoppingCartViewModel
    {

        public int OrderId { get; set; }
        public IEnumerable<ShoppingCartLineItemViewModel> ShoppingCart { get; set; } 

        public ShoppingCartViewModel()
        {
            ShoppingCart = new List<ShoppingCartLineItemViewModel>();
        }

    }
}