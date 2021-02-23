using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
   public class Order : BaseEntity
    {


        public string firstName { get; set; }
        public string surName { get; set; }
        public string email { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public string orderStatus { get; set; }
        public virtual ICollection<OrderItem> orderItems { get; set; }

        public Order() {
            this.orderItems = new List<OrderItem>();
        }

    }
}
