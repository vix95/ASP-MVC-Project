using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KacikFryzjerski.Models
{
    public class ItemModels
    {
        public int Id { get; set; }
        public int Item_category_id { get; set; }
        [Required(ErrorMessage = "Wprowadź nazwę produktu")]
        [StringLength(100)]
        public string Item_name { get; set; }
        [Required(ErrorMessage = "Wprowadź opis produktu")]
        [StringLength(250)]
        public string Item_description { get; set; }
        public double Item_price { get; set; }
        [StringLength(200)]
        public string Item_image_path { get; set; }
        public DateTime Item_created_at { get; set; }

        public virtual CategoryModels Category { get; set; }
    }
}