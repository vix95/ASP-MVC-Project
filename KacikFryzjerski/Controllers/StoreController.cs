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
                StoreProducts = db.Products.OrderByDescending(x => x.Id).ToList(),
                StoreCategories = db.Categories.OrderByDescending(x => x.Category_name).ToList()
            };

            return View(home_view_model);
        }

        public ActionResult CategoryList(int category_id)
        {
            //var category = db.categories.Include("Category_Items").Where(x => x.Category_name.ToUpper() == category_name.ToUpper()).Single();
            //var items = category.Category_Items.ToList();
            var items = db.Products.OrderByDescending(x => x.Id).Where(x => x.Product_category_id == category_id).ToList();

            return View(items);
        }

        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var storeCategories = db.Categories.OrderByDescending(x => x.Category_name).ToList();
            return PartialView("_CategoryMenu", storeCategories);
        }

        public ActionResult ProductDetails(int product_id)
        {
            var product = db.Products.Find(product_id);
            return View(product);
        }
    }
}