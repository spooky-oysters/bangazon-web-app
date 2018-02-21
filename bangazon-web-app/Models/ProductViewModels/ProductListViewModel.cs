using Bangazon.Data;
using Bangazon.Models;
using System.Collections.Generic;

namespace bangazon.models.ProductViewModels
{
    public class ProductListViewModel
    {
        public Product product { get; set; }
        public IEnumerable<ProductListViewModel> TopProduct { get; set; }
        //public productlistviewmodel(ApplicationDbContext ctx)
        
    }
}
