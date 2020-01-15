using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KacikFryzjerski.Models
{
    public class OrderPositionModels
    {
        public int Id { get; set; }
        public int OrderPosition_order_id { get; set; }
        public int OrderPosition_product_id { get; set; }
        public int OrderPosition_quantity { get; set; }
        public double OrderPosition_price { get; set; }

        public virtual ProductModels Product { get; set; }
        public virtual OrderModels Order { get; set; }
    }
}