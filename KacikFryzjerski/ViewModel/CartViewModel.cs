using KacikFryzjerski.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KacikFryzjerski.ViewModel
{
    public class CartViewModel
    {
        public List<CartPosition> cartPositions { get; set; }
        public double TotalPrice { get; set; }
    }
}