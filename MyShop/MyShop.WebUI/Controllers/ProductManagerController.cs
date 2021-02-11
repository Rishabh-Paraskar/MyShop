using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        public ProductManagerController() {
            context = new ProductRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List <Product> product= context.Collection().ToList();
            return View(product);
        }

        public ActionResult Create() {
            Product pro = new Product(); 
            return View(pro);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else {
                context.Insert(product);
                context.commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id) {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();

            }
            else {
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product, string Id) {
            Product productTOEdit = context.Find(Id);
            if (productTOEdit == null)
            {
                return HttpNotFound();

            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                else {
                    productTOEdit.Category = product.Category;
                    productTOEdit.Description = product.Description;
                    productTOEdit.Name = product.Name;
                    productTOEdit.Price = product.Price;
                    productTOEdit.Image = product.Image;

                    context.commit();
                    return RedirectToAction("Index");
                }
            }

        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();

            }
            else
            {
                return View(productToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();

            }
            else
            {
                context.Delete(Id);
                context.commit();
                return RedirectToAction("index");
            }
        }

    }
}