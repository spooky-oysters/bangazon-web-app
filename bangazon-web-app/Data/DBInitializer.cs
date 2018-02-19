using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bangazon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bangazon.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            using (context)
            {
                // clear the database
                //context.Database.ExecuteSqlCommand("DELETE FROM [ProductType]");
                context.Database.ExecuteSqlCommand("DELETE FROM [LineItem]");
                context.Database.ExecuteSqlCommand("DELETE FROM [Order]");
                context.Database.ExecuteSqlCommand("DELETE FROM [PaymentType]");
                context.Database.ExecuteSqlCommand("DELETE FROM [Product]");

                // Look for any products types.
                if (!context.ProductType.Any())
                {
                    var productTypes = new ProductType[]
                    {
                        new ProductType {
                            Label = "Electronics"
                        },
                        new ProductType {
                            Label = "Appliances"
                        },
                        new ProductType {
                            Label = "Housewares"
                        },
                    };

                    foreach (ProductType i in productTypes)
                    {
                        context.ProductType.Add(i);
                    }
                    context.SaveChanges();
                }

                // create the users
                ApplicationUser uno = userManager.FindByNameAsync("UNO@UNO.COM").Result;
                ApplicationUser dos = userManager.FindByNameAsync("DOS@DOS.COM").Result;
                ApplicationUser tres = userManager.FindByNameAsync("TRES@TRES.COM").Result;

                if (!context.Product.Any())
                {

                    Product[] products = new Product[] {

                    new Product
                        {
                            Quantity = 5,
                            DateCreated = DateTime.Now,
                            Description = "Uno Housewares 1",
                            Title="Uno Housewares 1",
                            Price=140.50,
                            User=uno,
                            ProductTypeId= context.ProductType.Single(p => p.Label == "Housewares").ProductTypeId,
                            LocalDelivery=true,
                            City="Nashville, TN",
                            Image=null
                        },
                        new Product
                        {
                            Quantity = 5,
                            DateCreated = DateTime.Now,
                            Description = "Uno Housewares 2",
                            Title="Uno Housewares 2",
                            Price=160.50,
                            User=uno,
                            ProductTypeId= context.ProductType.Single(p => p.Label == "Appliances").ProductTypeId,
                            LocalDelivery=false,
                            Image=null
                        },
                        new Product
                        {
                            Quantity = 5,
                            DateCreated = DateTime.Now,
                            Description = "Dos Electronics 1",
                            Title="Dos Electronics 1",
                            Price=50.00,
                            User=dos,
                            ProductTypeId= context.ProductType.Single(p => p.Label == "Electronics").ProductTypeId,
                            LocalDelivery=true,
                            City="Nashville, TN",
                            Image=null
                        },
                        new Product
                        {
                            Quantity = 5,
                            DateCreated = DateTime.Now,
                            Description = "Dos Electronics 2",
                            Title="Dos Electronics 2",
                            Price=160.50,
                            User=uno,
                            ProductTypeId= context.ProductType.Single(p => p.Label == "Electronics").ProductTypeId,
                            LocalDelivery=false,
                            Image=null
                        },
                        new Product
                        {
                            Quantity = 5,
                            DateCreated = DateTime.Now,
                            Description = "Tres Appliances 1",
                            Title="Tres Appliances 1",
                            Price=60.50,
                            User=uno,
                            ProductTypeId= context.ProductType.Single(p => p.Label == "Electronics").ProductTypeId,
                            LocalDelivery=false,
                            Image=null
                        },
                         new Product
                        {
                            Quantity = 5,
                            DateCreated = DateTime.Now,
                            Description = "Tres Appliances 2",
                            Title="Tres Appliances 2",
                            Price=160.50,
                            User=uno,
                            ProductTypeId= context.ProductType.Single(p => p.Label == "Appliances").ProductTypeId,
                            LocalDelivery=true,
                            City = "Portland, TN",
                            Image=null
                        }
                    
                    };

                    foreach (Product i in products)
                    {
                        context.Product.Add(i);
                    }
                    context.SaveChanges();
                } // End Product

                if (!context.PaymentType.Any())
                {

                    PaymentType[] paymentTypes = new PaymentType[]
                    {
                        new PaymentType {
                            DateCreated = DateTime.Now,
                            Description = "unocard",
                            AccountNumber = "12345",
                            User = uno
                        },
                        new PaymentType {
                            DateCreated = DateTime.Now,
                            Description = "Paypal",
                            AccountNumber = "uno@uno.com",
                            User = uno
                        },
                        new PaymentType {
                            DateCreated = DateTime.Now,
                            Description = "doscard",
                            AccountNumber = "12345",
                            User = dos
                        },
                        new PaymentType {
                            DateCreated = DateTime.Now,
                            Description = "Paypal",
                            AccountNumber = "dos@dos.com",
                            User = dos
                        },
                        new PaymentType {
                            DateCreated = DateTime.Now,
                            Description = "trescard",
                            AccountNumber = "12345",
                            User = dos
                        },
                        new PaymentType {
                            DateCreated = DateTime.Now,
                            Description = "Paypal",
                            AccountNumber = "tres@tres.com",
                            User = dos
                        },


                    };

                    foreach (PaymentType i in paymentTypes)
                    {
                        context.PaymentType.Add(i);
                    }

                    context.SaveChanges();

                }

                // generic line items
                Product uno1 = context.Product.Where(p => p.Title == "Dos Electronics 1").Single();
                Product uno2 = context.Product.Where(p => p.Title == "Tres Appliances 1").Single();
                List<Product> productListUno1 = new List<Product>() { uno1, uno2};

                // uno's closed orders
                Product uno3 = context.Product.Where(p => p.Title == "Dos Electronics 2").Single();
                Product uno4 = context.Product.Where(p => p.Title == "Tres Appliances 2").Single();
                List<Product> productListUno2 = new List<Product>() { uno3, uno4 };

                // dos's closed orders
                Product dos1 = context.Product.Where(p => p.Title == "Uno Housewares 1").Single();
                Product dos2 = context.Product.Where(p => p.Title == "Tres Appliances 1").Single();
                List<Product> productListDos1 = new List<Product>() { dos1, dos2 };


                // dos's open order
                Product dos3 = context.Product.Where(p => p.Title == "Uno Housewares 2").Single();
                Product dos4 = context.Product.Where(p => p.Title == "Tres Appliances 2").Single();
                List<Product> productListDos2 = new List<Product>() { dos3, dos4 };

                // tres's closed orders
                Product tres1 = context.Product.Where(p => p.Title == "Uno Housewares 1").Single();
                Product tres2 = context.Product.Where(p => p.Title == "Dos Electronics 1").Single();
                List<Product> productListTres1 = new List<Product>() { tres1, tres2 };

                // tres's open order
                Product tres3 = context.Product.Where(p => p.Title == "Uno Housewares 2").Single();
                Product tres4 = context.Product.Where(p => p.Title == "Dos Electronics 2").Single();
                List<Product> productListTres2 = new List<Product>() { tres3, tres4 };



                // Generate orders
                if (!context.Order.Any())
                {

                    Dictionary<Order, List<Product>> orders = new Dictionary<Order, List<Product>>()
                    {
                        // uno order 1 that is completed
                        { new Order {
                           User = uno,
                           CreatedDate = new DateTime(2017, 1, 18),
                           CompletedDate = new DateTime(2017, 1, 18),
                           PaymentTypeId = context.PaymentType.Where(p => p.User == uno && p.Description == "Paypal").Single().PaymentTypeId,
                         },
                           productListUno1
                         },
                        { new Order {
                           User = uno,
                           CreatedDate = DateTime.Now,
                           CompletedDate = null,
                           PaymentTypeId = null
                         },
                           productListUno2
                         },

                        // dos orders
                        { new Order {
                           User = dos,
                           CreatedDate = new DateTime(2017, 1, 18),
                           CompletedDate = new DateTime(2017, 1, 18),
                           PaymentTypeId = context.PaymentType.Where(p => p.User == dos && p.Description == "doscard").Single().PaymentTypeId,
                         },
                           productListDos1
                         },
                        { new Order {
                           User = dos,
                           CreatedDate = DateTime.Now,
                           CompletedDate = null,
                           PaymentTypeId = null
                         },
                           productListDos2
                         },
                        { new Order {
                           User = tres,
                           CreatedDate = new DateTime(2017, 1, 18),
                           CompletedDate = new DateTime(2017, 1, 18),
                           PaymentTypeId = context.PaymentType.Where(p => p.Description == "trescard").Single().PaymentTypeId,
                         },
                           productListTres1
                         },
                        { new Order {
                           User = tres,
                           CreatedDate = DateTime.Now,
                           CompletedDate = null,
                           PaymentTypeId = null
                         },
                           productListTres2
                         }


                    };


                    foreach (var kvp in orders)
                    {

                        context.Order.Add(kvp.Key);
                        context.SaveChanges();
                        int id = kvp.Key.OrderId;

                        // add line items
                        foreach (var p in kvp.Value)
                        {
                            LineItem li = new LineItem();
                            li.OrderId = id;
                            li.ProductId = p.ProductId;
                            context.LineItem.Add(li);
                            
                        }

                    }

                    context.SaveChanges();


                }
            }
        }
    }
}