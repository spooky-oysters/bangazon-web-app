﻿//using System.Collections.Generic;
//using System.Linq;
//using Bangazon.Data;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using System;

//namespace Bangazon.Models.ProductViewModels
//{
//    public class ProductListViewModel
//    {
//        //public List<SelectListItem> ProductTypeId { get; set; }
//        public Product Product { get; set; }
//        public ProductListViewModel(ApplicationDbContext ctx)
//        {
//            // Creating SelectListItems will be used in a @Html.DropDownList
//            // control in a Razor template. 
//            this.ProductTypeId = ctx.ProductType
//                .OrderBy(l => l.Label)
//                .AsEnumerable()
//                .Select(li => new SelectListItem
//                {
//                    Text = li.Label,
//                    Value = li.ProductTypeId.ToString()
//                }).ToList();

//            this.ProductTypeId.Insert(0, new SelectListItem
//            {
//                Text = "Choose category...",
//                Value = "0"
//            });
//        }
//    }
//}