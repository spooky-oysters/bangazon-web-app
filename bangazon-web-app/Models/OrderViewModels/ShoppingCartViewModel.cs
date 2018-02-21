using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.OrderViewModels
{
    public class ShoppingCartViewModel
    {

        public int OrderId { get; set; }
        public IEnumerable<ShoppingCartLineItemViewModel> ShoppingCart { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double ShoppingCartTotal { get; set; }

        public ShoppingCartViewModel()
        {
            ShoppingCart = new List<ShoppingCartLineItemViewModel>();
        }

    }
}
