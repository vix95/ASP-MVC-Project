using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KacikFryzjerski.Models
{
    public class CartPosition
    {
        public ProductModels Product { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
    }
}