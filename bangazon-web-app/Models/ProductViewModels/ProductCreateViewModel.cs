using System.Collections.Generic;
using System.Linq;
using Bangazon.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models.ProductViewModels
{
    public class ProductCreateViewModel
    {
        [Required]
        public List<SelectListItem> ProductTypeId { get; set; }

        [Required]
        public Product Product { get; set; }

        public ProductCreateViewModel(ApplicationDbContext ctx)
        {
            // Creating SelectListItems will be used in a @Html.DropDownList
            // control in a Razor template. 
            this.ProductTypeId = ctx.ProductType
                .OrderBy(l => l.Label)
                .AsEnumerable()
                .Select(li => new SelectListItem
                {
                    Text = li.Label,
                    Value = li.ProductTypeId.ToString()
                }).ToList();

            // adds initial drop down select item
            this.ProductTypeId.Insert(0, new SelectListItem
            {
                Text = "Choose Category...",
                Value = "0"
            });
        }
    }
}
