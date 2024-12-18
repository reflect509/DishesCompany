using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DishesCompany
{
    public static class DatabaseControl
    {
        public static Users GetUser(string login, string pass)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Users.FirstOrDefault(p => p.Login == login && p.Pass == pass);
            }
        }
        public static bool CheckUser(string login)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                if (ctx.Users.FirstOrDefault(p => p.Login == login) is not null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void AddUserRecord(string full_name, string login, string password)
        {
            using (DbAppContext ctx = new DbAppContext())
            {                
                ctx.Users.Add(new Users(full_name, login, password));
                ctx.SaveChanges();
            }
        }

        public static void AddProductRecord(Products product)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Products.Add(product);
                ctx.SaveChanges();
            }
        }

        public static void EditProductRecord(Products product)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                Products _product = ctx.Products.First(p => p.Articul == product.Articul);

                _product.Articul = product.Articul;
                _product.Product_name = product.Product_name;
                _product.Product_type = product.Product_type;
                _product.Amount = product.Amount;
                _product.Measurement_unit = product.Measurement_unit;
                _product.Manufacturer = product.Manufacturer;
                _product.Supplier = product.Supplier;
                _product.Product_cost = product.Product_cost;
                _product.Description = product.Description;
                _product.Image = product.Image;

                ctx.SaveChanges();
            }
        }
        

        public static List<Products> GetProducts()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Products.ToList();
            }
        }
        public static List<string> GetManufacturers()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Products.Select(p => p.Manufacturer).Distinct().ToList();
            }
        }

        public static List<Products> FilteredProducts(string manufacturer, List<Products> searchedProducts)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return searchedProducts.Where(p => p.Manufacturer == manufacturer).ToList();
            }
        }

        public static List<Products> SortingProducts(string sortingDirection, List<Products> searchedAndFilteredProducts)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                if (sortingDirection == "asc")
                {
                    return searchedAndFilteredProducts.OrderBy(p => p.Product_cost).ToList();

                }
                else
                {
                    return searchedAndFilteredProducts.OrderByDescending(p => p.Product_cost).ToList();
                }
            }
        }

        public static int GetCount(List<Products> products)
        {
            using(DbAppContext ctx = new DbAppContext())
            {
                return products.Count();
            }
        }

        public static int GetCount()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return ctx.Products.Count();
            }
        }

        public static void DeleteProduct(Products product)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Products.Remove(product);
                ctx.SaveChanges();
            }
        }

        public static bool ValidProduct(string articul)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                return !ctx.Product_orders.Any(p => p.Product_articul == articul);
            }
        }
    }
}
