using System;
using FoodOrder.Server.Domain.Entities;
using FoodOrder.Server.Persistence.Mapping;
using Microsoft.EntityFrameworkCore;

namespace FoodOrder.Server.Persistence.Context
{
    public class FoodOrderDbContext :DbContext
    {
        public FoodOrderDbContext(DbContextOptions<FoodOrderDbContext> options):base(options)
        {
        }

        public virtual DbSet<Users> Users{ get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
