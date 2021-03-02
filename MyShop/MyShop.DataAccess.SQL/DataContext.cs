using System;
using System.Collections.Generic;
using System.Data.Entity;
using MyShop.Core.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyShop.DataAccess.SQL
{
   public class DataContext : DbContext
    {
        public DataContext()
            : base("DefaultConnection") {

        }

        public DbSet<Product> product{ get; set; }
        public DbSet<ProductCategory> productCategory { get; set; }
        public DbSet<Basket> basket { get; set; }
        public DbSet<BasketItem> basketItem { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Order> orders { get; set; }


    }
}
