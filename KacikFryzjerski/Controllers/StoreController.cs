using KacikFryzjerski.DAL;
using KacikFryzjerski.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KacikFryzjerski.Controllers
{
    public class StoreController : Controller
    {
        private readonly DbContext db = new DbContext();
        public ActionResult Products()
        {
            var store_items = db.items.OrderByDescending(x => x.Item_created_at).ToList();
            var home_view_model = new HomeViewModel()
            {
                StoreItems = store_items
            };

            return View(home_view_model);
        }
    }
}