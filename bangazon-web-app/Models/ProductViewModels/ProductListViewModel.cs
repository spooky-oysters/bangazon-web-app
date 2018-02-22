// Author: John Dulaney
// Description: This viewmodel helps the Index populate the topproduct listings
using System.Collections.Generic;

namespace Bangazon.Models.ProductViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> TopProduct { get; set; }
    }
}
