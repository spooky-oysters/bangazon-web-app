using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Bangazon.Models.ProductViewModels
{
    public class ProductDetailViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int  AvailableQuantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }
    }
}
