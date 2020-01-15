using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KacikFryzjerski.Models;

namespace KacikFryzjerski.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<ProductModels> Bestsellers { get; set; }
        public IEnumerable<ProductModels> StoreProducts { get; set; }
        public IEnumerable<CategoryModels> StoreCategories { get; set; }
    }
}