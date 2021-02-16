using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.Services;
using MyShop.WebUI.Controllers;
using MyShop.WebUI.Tests.Mocks;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTests
    {
        [TestMethod]
        public void canAddBasketItems()
        {
            //setup
            IRepository<Basket> basket = new MockContext<Basket>();
            IRepository<Product> product = new MockContext<Product>();

            var httpcontext = new MockHttpContext();

            IBasketService basketService = new BasketService(product,basket);
            var controller = new BasketController(basketService);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpcontext, new System.Web.Routing.RouteData(), controller);
            
            //Act
            //basketService.addToBasket(httpcontext, "1");
            controller.addToBasket("1");

           Basket bas= basket.collection().FirstOrDefault();
           
            //Assert
            Assert.IsNotNull(bas);
            Assert.AreEqual(1, bas.basketItems.Count);
            Assert.AreEqual("1", bas.basketItems.ToList().FirstOrDefault().productId);

        }
        [TestMethod]
        public void canGetSummaryViewModel() {
            IRepository<Product> product = new MockContext<Product>();
            IRepository<Basket> basket = new MockContext<Basket>();

            product.insert(new Product() {Id="1", Price= 10.00m});
            product.insert(new Product() { Id = "2", Price = 15.00m });

            Basket bas = new Basket();
            bas.basketItems.Add(new BasketItem() { productId = "1", quantity = 2 });
            bas.basketItems.Add(new BasketItem() { productId = "2", quantity = 3 });
            basket.insert(bas);

            IBasketService basketService = new BasketService(product,basket);

            var controller = new BasketController(basketService);
            var httpContext = new MockHttpContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = bas.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);


            var result = controller.basketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(5, basketSummary.basketCount);
            Assert.AreEqual(65.00m, basketSummary.basketTotal);
        }

    }
}
