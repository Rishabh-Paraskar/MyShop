using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {

        IRepository<Product> context;
        IRepository<ProductCategory> productcategories;
        public HomeController(IRepository<Product> productcontext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productcontext;
            productcategories = productCategoryContext;
        }
        public ActionResult Index(string category=null)
        {
            List<Product> products;
            List<ProductCategory> categories = productcategories.collection().ToList();

            if (category == null) {
               products = context.collection().ToList();

            }
            else
            {
                products = context.collection().Where(p => p.Category== category).ToList();

            }
            ProductListViewModel model = new ProductListViewModel();
            model.product = products;
            model.productCategories =  categories;
            return View(model);
        }

        public ActionResult Details(String Id) {
            Product produts = context.find(Id);
            if (produts == null)
            {
                return HttpNotFound();
            }
            else {
                return View(produts);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}