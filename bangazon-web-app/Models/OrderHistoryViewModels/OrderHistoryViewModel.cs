﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.OrderHistoryViewModels
{
    public class OrderHistoryViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}
