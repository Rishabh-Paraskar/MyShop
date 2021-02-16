using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Services
{
    public class BasketService : IBasketService
    {
        IRepository<Product> productContext;
        IRepository<Basket> basketContext;

        public const string basketSessionName = "eCommerceBasket";

        public BasketService(IRepository<Product> ProductContext, IRepository<Basket> BasketContext) {
            this.productContext = ProductContext;
            this.basketContext = BasketContext;
        }

        private Basket getBasket(HttpContextBase httpContext, bool createIfNull) {
            HttpCookie cookie = httpContext.Request.Cookies.Get(basketSessionName);

            Basket basket = new Basket();
            if (cookie != null)
            {
                string basketId = cookie.Value;
                if (!string.IsNullOrEmpty(basketId))
                {
                    basket = basketContext.find(basketId);
                }
                else
                {
                    if (createIfNull)
                    {
                        basket = createNewBasket(httpContext);
                    }

                }
            }
            else {
                if (createIfNull)
                {
                    basket = createNewBasket(httpContext);
                }

            }
            return basket;
        }




        private Basket createNewBasket(HttpContextBase httpContext)
        {
           Basket basket = new Basket();
            basketContext.insert(basket);
            basketContext.commit();

            HttpCookie cookie = new HttpCookie(basketSessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }


        public void addToBasket(HttpContextBase httpContext, string ProductId)
        {
            Basket basket = getBasket(httpContext, true);
            BasketItem item = basket.basketItems.FirstOrDefault(i => i.productId == ProductId);

            if (item == null)
            {

                item = new BasketItem()
                {
                    basketId = basket.Id,
                    productId = ProductId,
                    quantity = 1
                };
                basket.basketItems.Add(item);
            }
            else {
                item.quantity = item.quantity + 1;
            }
            basketContext.commit();
           
        }

        public void removeFromBasket(HttpContextBase httpContext, string itemId) {
            Basket basket = getBasket(httpContext, true);
            BasketItem item = basket.basketItems.FirstOrDefault(i => i.Id == itemId);

            if (item != null) {
                basket.basketItems.Remove(item);
                basketContext.commit();
            }
        }

        public List<BasketItemViewModel> getBasketItem(HttpContextBase httpContext) {
            Basket basket = getBasket(httpContext, false);

            if (basket != null)
            {
                var result = (from b in basket.basketItems
                              join p in productContext.collection() on b.productId equals p.Id
                              select new BasketItemViewModel()
                              {
                                  id = b.Id,
                                  quantity = b.quantity,
                                  productName = p.Name,
                                  image = p.Image,
                                  price = p.Price

                              }
                              ).ToList();
                return result;
            }
            else {
                return new List<BasketItemViewModel>();
            }
        }

        public BasketSummaryViewModel getBasketSummary(HttpContextBase httpContext)
        {
            Basket basket = getBasket(httpContext, false);
            BasketSummaryViewModel model = new BasketSummaryViewModel(0,0);
            if (basket != null)
            {
                int? basketCount = (from item in basket.basketItems
                                    select item.quantity).Sum();
                decimal? basketTotal = (from item in basket.basketItems
                                        join p in productContext.collection() on item.productId equals p.Id
                                        select item.quantity * p.Price).Sum();
                model.basketCount = basketCount ?? 0;
                model.basketTotal = basketTotal ?? decimal.Zero;
                return model;
            }
            else
            {
                return model;
            }
        }
    }
}
