using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderManagerController : Controller
    {
        IOrderService orderService;

        public OrderManagerController(IOrderService OrderService) {
            this.orderService = OrderService;
        }

        // GET: OrderManager
        public ActionResult Index()
        {
            List<Order> orders = orderService.getOrderList();
            return View(orders);
        }
        public ActionResult updateOrder(string id) {

            ViewBag.statusList = new List<string>()
            {
                "Order Created",
                "Payment Processed",
                "Order Shipped",
                "Order Completed"
            };
            Order order = orderService.getOrder(id);
            return View(order);
        }
        [HttpPost]
        public ActionResult updateOrder(Order updatedOrder, string id) {
            Order order = orderService.getOrder(id);

            order.orderStatus = updatedOrder.orderStatus;
            orderService.updateOrder(order);
            return RedirectToAction("Index");
        }
    }
}