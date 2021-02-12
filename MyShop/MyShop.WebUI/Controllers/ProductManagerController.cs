﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;
namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        InMemoryRepository<Product> context;
        InMemoryRepository<ProductCategory> productcategories;
        public ProductManagerController() {
            context = new InMemoryRepository<Product>();
            productcategories = new InMemoryRepository<ProductCategory>();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List <Product> product= context.collection().ToList();
            return View(product);
        }

        public ActionResult Create() {
            ProductManagerViewmodel productManagerviewModel = new ProductManagerViewmodel();
            productManagerviewModel.product = new Product();
            productManagerviewModel.productCategories = productcategories.collection();
            return View(productManagerviewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else {
                context.insert(product);
                context.commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id) {
            Product product = context.find(Id);
            if (product == null)
            {
                return HttpNotFound();

            }
            else {
                ProductManagerViewmodel viewModel = new ProductManagerViewmodel();
                viewModel.product = product;
                viewModel.productCategories = productcategories.collection();
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product, string Id) {
            Product productTOEdit = context.find(Id);
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
            Product productToDelete = context.find(Id);
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
            Product productToDelete = context.find(Id);
            if (productToDelete == null)
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