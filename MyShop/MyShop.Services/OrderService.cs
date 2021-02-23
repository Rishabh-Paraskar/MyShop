using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services
{
   public class OrderService : IOrderService
    {
        IRepository<Order> orderContext;

        public OrderService(IRepository<Order> orderCon) {
            this.orderContext = orderCon;
        }

        public void createOrder(Order baseOrder, List<BasketItemViewModel> basketItems)
        {
            foreach (var item in basketItems) {
                baseOrder.orderItems.Add(new OrderItem() {
                
                productId=item.id,
                image= item.image,
                price=item.price,
                productName=item.productName,
                quantity=item.quantity
                
                });
            }
            orderContext.insert(baseOrder);
            orderContext.commit();
        }

        public List<Order> getOrderList() {

            return orderContext.collection().ToList();
        }

        public Order getOrder(string id) {

            return orderContext.find(id);
        }

        public void updateOrder(Order updatedOrder) {

            orderContext.update(updatedOrder);
            orderContext.commit();
        }
    }     
}
