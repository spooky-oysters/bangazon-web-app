using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.OrderHistoryViewModels
{
    public class OrderHistoryDetailViewModel
    {
        [Required]
        public IEnumerable<LineItem> Products { get; set; }

        [Required]
        public Order Order { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double OrderTotal { get; set; }
    }
}
