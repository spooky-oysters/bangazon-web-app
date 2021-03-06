﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bangazon.Models;

namespace Bangazon.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}

            public DbSet<Order> Order { get; set; }
            public DbSet<LineItem> LineItem { get; set; }
            public DbSet<PaymentType> PaymentType {
            get; set; }
            public DbSet<Product> Product { get; set; }
            public DbSet<ProductType> ProductType { get; set; }
            

        

    protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Product>()
                .Property(p => p.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            builder.Entity<Order>()
                .Property(o => o.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Entity<PaymentType>()
                .Property(p => p.DateCreated)
                .HasDefaultValueSql("GETDATE()");

        }
          

    }
}
