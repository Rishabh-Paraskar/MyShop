using MyShop.Core.Contracts;
using MyShop.Core.Models;
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
        IOrderService orderService;
        IRepository<Customer> customers;

        public BasketController(IBasketService BasketService, IOrderService OrderService, IRepository<Customer> customer) {
            this.basketService = BasketService;
            this.orderService = OrderService;
            this.customers = customer;
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

        [Authorize]
        public ActionResult checkOut() {
            Customer cus = customers.collection().FirstOrDefault(i => i.email== User.Identity.Name);
            if (cus != null)
            {

                Order order = new Order()
                {

                    email = cus.email,
                    city = cus.city,
                    state = cus.state,
                    street = cus.street,
                    firstName = cus.firstName,
                    surName = cus.lastName,
                    zipCode = cus.zipCode,

                };
                return View(order);
            }
            else {
                return RedirectToAction("Error");
            }
            
        }

        [HttpPost]
        [Authorize]
        public ActionResult checkOut(Order order)
        {

           var basketItems = basketService.getBasketItem(this.HttpContext);
            order.orderStatus = "Order created";
            order.email = User.Identity.Name;
            order.orderStatus = "Payment Processed";
            orderService.createOrder(order, basketItems);
            basketService.clearBasket(this.HttpContext);

            return RedirectToAction("Thankyou", new { orderId= order.Id });
        }

        public ActionResult Thankyou(string orderId) {

            ViewBag.orderId = orderId;
            return View();
        }
    }
}