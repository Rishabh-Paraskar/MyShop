using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Models;
using MyShop.Core.Contracts;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> context;

        public ProductCategoryManagerController(IRepository<ProductCategory> productCategoryContext) {
            context = productCategoryContext;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List <ProductCategory> productCategory = context.collection().ToList();
            return View(productCategory);
        }

        public ActionResult Create() {
            ProductCategory productCategory = new ProductCategory(); 
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else {
                context.insert(productCategory);
                context.commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id) {
            ProductCategory productCategory = context.find(Id);
            if (productCategory == null)
            {
                return HttpNotFound();

            }
            else {
                return View(productCategory);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string Id) {
            ProductCategory productCategoryTOEdit = context.find(Id);
            if (productCategoryTOEdit == null)
            {
                return HttpNotFound();

            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }
                else {
                    productCategoryTOEdit.Category = productCategory.Category;
                    context.commit();
                    return RedirectToAction("Index");
                }
            }

        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();

            }
            else
            {
                return View(productCategoryToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();

            }
            else
            {
                context.delete(Id);
                context.commit();
                return RedirectToAction("index");
            }
        }
    }
}