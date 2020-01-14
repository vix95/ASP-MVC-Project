using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KacikFryzjerski.Models;

namespace KacikFryzjerski.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<ItemModels> Bestsellers { get; set; }
    }
}