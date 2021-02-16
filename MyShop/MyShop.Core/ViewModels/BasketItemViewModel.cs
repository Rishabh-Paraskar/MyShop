using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
   public class BasketItemViewModel
    {
        public string id { get; set; }
        public int quantity { get; set; }
        public string productName { get; set; }
        public decimal price { get; set; }
        public string image { get; set; }

    }
}
