using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KacikFryzjerski.Models
{
    public class OrderProductModels
    {
        public int Id { get; set; }
        public int OrderProduct_order_id { get; set; }
        public int OrderProduct_item_id { get; set; }
        public int OrderProduct_quantity { get; set; }
        public decimal OrderProduct_price { get; set; }

        public virtual ProductModels OrderProduct_productsModels { get; set; }
        public virtual OrderModels OrderProduct_orderModels { get; set; }
    }
}