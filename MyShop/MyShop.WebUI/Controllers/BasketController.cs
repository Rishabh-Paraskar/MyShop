using MyShop.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IBasketService basketService;

        public BasketController(IBasketService BasketService) {
            this.basketService = BasketService;
        }
        // GET: Basket
        public ActionResult Index()
        {
            var model = basketService.getBasketItem(this.HttpContext);
            return View(model);
        }

        public ActionResult addToBasket(String id) {
            basketService.addToBasket(this.HttpContext, id);
            return RedirectToAction("Index");
        }

        public ActionResult removeFromBasket(String id)
        {
            basketService.removeFromBasket(this.HttpContext, id);
            return RedirectToAction("Index");
        }

        public PartialViewResult basketSummary() {
            var basketSummary = basketService.getBasketSummary(this.HttpContext);
            return PartialView(basketSummary);
        }
    }
}