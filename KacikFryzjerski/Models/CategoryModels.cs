using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KacikFryzjerski.Models
{
    public class CategoryModels
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Wprowadź nazwę kategorii")]
        [StringLength(100)]
        public string Category_name { get; set; }

        public virtual ICollection<ProductModels> Category_Items { get; set; }
    }
}