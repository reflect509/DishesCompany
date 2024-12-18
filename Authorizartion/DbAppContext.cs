using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishesCompany
{
    public class DbAppContext : DbContext
    {
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<DeliveryPlaces> DeliveryPlaces { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Product_orders> Product_orders { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Database=dish_sale");
        }        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Приведение имен таблиц и столбцов к нижнему регистру
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToLower());

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToLower());
                }
            }


            modelBuilder.Entity<Users>().HasOne(p => p.Role_entity)
                                        .WithMany(p => p.User_entities)
                                        .HasForeignKey(p => p.Role_id);
                

            modelBuilder.Entity<Orders>().HasOne(p => p.User_entity)
                                         .WithMany(p => p.Order_entities)
                                         .HasForeignKey(p => p.User_id);

            modelBuilder.Entity<Orders>().HasOne(p => p.DeliveryPlace_entity)
                                         .WithMany(p => p.Order_entities)
                                         .HasForeignKey(p => p.Delivery_place_id);


            modelBuilder.Entity<Product_orders>().HasOne(p => p.Order_entity)
                                                 .WithMany(p => p.Product_order_entities)
                                                 .HasForeignKey(p => p.Order_id);

            modelBuilder.Entity<Product_orders>().HasOne(p => p.Product_entity)
                                                 .WithMany(p => p.Product_order_entities)
                                                 .HasPrincipalKey(p => p.Articul);

            modelBuilder.Entity<Product_orders>().HasKey(p => new { p.Product_articul, p.Order_id });
        } 
    }
}
