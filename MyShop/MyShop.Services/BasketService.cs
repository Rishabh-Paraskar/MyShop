using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Services
{
    public class BasketService 
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
    }
}
