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
    }
}
