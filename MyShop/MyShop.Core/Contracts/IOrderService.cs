﻿using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Contracts
{
   public interface IOrderService
    {
         void createOrder( Order baseOrder, List<BasketItemViewModel> basketItems);
        List<Order> getOrderList();
        Order getOrder(string id);
        void updateOrder(Order updatedOrder);

    }
}
