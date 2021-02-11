using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;
namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache objectCache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository() {
            products = objectCache["products"] as List<Product>;
            if (products==null) {
                products = new List<Product>();
            }
        }

        public void commit() {

            objectCache["products"] = products;
        }

        public void Insert(Product p) {
            products.Add(p);
        }
        public void Update(Product prod)
        {
            Product productToUpdate = products.Find(p => p.Id == prod.Id);
            if (productToUpdate != null)
            {
                productToUpdate = prod;
            }
            else {
                throw new Exception("Product not Found");
            }
        }

        public Product Find(string Id) {
            Product product = products.Find(p => p.Id == Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not Found");
            }
        }

        public IQueryable<Product> Collection() {
            return products.AsQueryable();
        }

        public void Delete(string Id) {
            Product productToDelete = products.Find(p => p.Id == Id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else {
                throw new Exception("Product not Found");
            }
        } 
    }
}
