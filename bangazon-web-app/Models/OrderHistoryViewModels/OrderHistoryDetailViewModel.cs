using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.OrderHistoryViewModels
{
    public class OrderHistoryDetailViewModel
    {
        public IEnumerable<LineItem> Products { get; set; }
        public Order Order { get; set; }
        public double Total { get; set; }
    }
}
