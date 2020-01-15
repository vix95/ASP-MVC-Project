using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KacikFryzjerski.Models
{
    public class OrderModels
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Wprowadź imię")]
        [StringLength(100)]
        public string Order_name { get; set; }
        [Required(ErrorMessage = "Wprowadź nazwisko")]
        [StringLength(100)]
        public string Order_surname { get; set; }
        [Required(ErrorMessage = "Wprowadź miasto")]
        [StringLength(100)]
        public string Order_city { get; set; }
        [Required(ErrorMessage = "Wprowadź kod pocztowy")]
        [StringLength(7)]
        public string Order_postcode { get; set; }
        [Required(ErrorMessage = "Wprowadź adres")]
        [StringLength(100)]
        public string Order_address { get; set; }
        [Required(ErrorMessage = "Wprowadź numer mieszkania")]
        [StringLength(100)]
        public string Order_address_number { get; set; }
        [Required(ErrorMessage = "Wprowadź telefon")]
        [StringLength(100)]
        public string Order_phone { get; set; }
        [Required(ErrorMessage = "Wprowadź adres email")]
        [StringLength(100)]
        public string Order_email { get; set; }
        public DateTime Order_ordered_at { get; set; }
        public OrderStatus Order_order_status { get; set; }
        public decimal Order_total_order_price { get; set; }

        List<OrderItemModels> Order_orderItems { get; set; }
    }

    public enum OrderStatus
    {
        new_order,
        on_pending,
        waiting_for_delivery,
        realized
    }
}