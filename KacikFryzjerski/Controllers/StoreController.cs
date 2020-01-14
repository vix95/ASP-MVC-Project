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
            var home_view_model = new HomeViewModel()
            {
                StoreItems = db.items.OrderByDescending(x => x.Item_created_at).ToList(),
                StoreCategories = db.categories.OrderByDescending(x => x.Category_name).ToList()
            };

            return View(home_view_model);
        }

        public ActionResult CategoryList(string category_name)
        {
            var category = db.categories.Include("ItemModels").Where(x => x.Category_name.ToUpper() == category_name.ToUpper()).Single();
            var items = category.Category_Items.ToList();

            return View(items);
        }

        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var storeCategories = db.categories.OrderByDescending(x => x.Category_name).ToList();
            return PartialView("_CategoryMenu", storeCategories);
        }
    }
}