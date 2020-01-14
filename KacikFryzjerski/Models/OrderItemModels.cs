using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KacikFryzjerski.Models
{
    public class OrderItemModels
    {
        public int Id { get; set; }
        public int OrderItem_order_id { get; set; }
        public int OrderItem_item_id { get; set; }
        public int OrderItem_quantity { get; set; }
        public decimal Orderitem_price { get; set; }

        public virtual ItemModels OrderItem_itemModels { get; set; }
        public virtual OrderModels OrderItem_orderModels { get; set; }
    }
}