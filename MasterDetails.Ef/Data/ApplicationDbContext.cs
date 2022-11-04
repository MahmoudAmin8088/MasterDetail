using MasterDetails.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetails.Ef.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> items { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Customer> Customer { get; set; }  

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<OrderItems>()
            //    .HasKey(sc => new { sc.OrderId, sc.ItemId });

            builder.Entity<OrderItems>()
                .HasOne<Order>(sc=>sc.Order)
                .WithMany(s=>s.OrderItems)
                .HasForeignKey(sc=>sc.OrderId);

            builder.Entity<OrderItems>()
                .HasOne<Item>(sc => sc.Item)
                .WithMany(s => s.OrderItems)
                .HasForeignKey(sc => sc.ItemId);

            base.OnModelCreating(builder);
        }
    }
}
