using EcommerceSite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSite.Data
{
    public class ItemContext : IdentityDbContext<AppUser>
    {
        public ItemContext(DbContextOptions<ItemContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserItems)
                .HasForeignKey(pt => pt.UserID);

            modelBuilder.Entity<UserPurchased>()
                .HasKey(up => new { up.UserID, up.ItemNumber });

            modelBuilder.Entity<UserPurchased>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.PurchaseItems)
                .HasForeignKey(bc => bc.UserID);

            modelBuilder.Entity<UserPurchased>()
                .HasOne(bc => bc.Item)
                .WithMany(c => c.Buyers)
                .HasForeignKey(bc => bc.ItemNumber);

        }
    }
}
