using MyShop.Core.Contracts;
using MyShop.Core.Models;
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
        public ActionResult Index()
        {
            List<Product> products = context.collection().ToList();
            return View(products);
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