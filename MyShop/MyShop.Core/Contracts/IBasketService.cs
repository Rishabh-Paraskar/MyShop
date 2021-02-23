using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Core.Contracts
{
   public interface IBasketService
    {
        void addToBasket(HttpContextBase httpContext, string ProductId);
        void removeFromBasket(HttpContextBase httpContext, string itemId);
        List<BasketItemViewModel> getBasketItem(HttpContextBase httpContext);
        BasketSummaryViewModel getBasketSummary(HttpContextBase httpContext);
        void clearBasket(HttpContextBase httpContext);

    }
}
