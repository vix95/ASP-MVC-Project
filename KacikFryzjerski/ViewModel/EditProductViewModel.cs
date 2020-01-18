using KacikFryzjerski.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KacikFryzjerski.ViewModel
{
    public class EditProductViewModel
    {
        public ProductModels Product { get; set; }
        public IEnumerable<CategoryModels> Categories { get; set; }
        public bool? Confirmation { get; set; }
    }
}