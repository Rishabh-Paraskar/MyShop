using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
   public class OrderItem : BaseEntity
    {
        public string orderId { get; set; }
        public string productId { get; set; }
        public string productName { get; set; }
        public decimal price { get; set; }
        public string image { get; set; }
        public int quantity { get; set; }

    }
}
