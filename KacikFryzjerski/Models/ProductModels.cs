using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KacikFryzjerski.Models
{
    public class ProductModels
    {
        public int Id { get; set; }
        public int Product_category_id { get; set; }
        [Required(ErrorMessage = "Wprowadź nazwę produktu")]
        [StringLength(100)]
        public string Product_name { get; set; }
        [Required(ErrorMessage = "Wprowadź opis produktu")]
        [StringLength(250)]
        public string Product_description { get; set; }
        public double Product_price { get; set; }
        [StringLength(200)]
        public string Product_image_path { get; set; }
        public DateTime Product_created_at { get; set; }
        public bool Product_is_bestseller { get; set; }

        public virtual CategoryModels Category { get; set; }
    }
}