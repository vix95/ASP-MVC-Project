using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KacikFryzjerski.ViewModel
{
    public class CartDeletingViewModel
    {
        public double CartTotalValue { get; set; }
        public int CartPositionsQuantity { get; set; }
        public int PositionsQuantityToDelete { get; set; }
        public int PositionIdToDelete { get; set; }
    }
}