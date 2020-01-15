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
                StoreProducts = db.products.OrderByDescending(x => x.Id).ToList(),
                StoreCategories = db.categories.OrderByDescending(x => x.Category_name).ToList()
            };

            return View(home_view_model);
        }

        public ActionResult CategoryList(int category_id)
        {
            var products = db.products.OrderByDescending(x => x.Id).Where(x => x.Product_category_id == category_id).ToList();

            return View(products);
        }

        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var storeCategories = db.categories.OrderByDescending(x => x.Category_name).ToList();
            return PartialView("_CategoryMenu", storeCategories);
        }

        public ActionResult ProductDetails(string product_id)
        {

            return View();
        }
    }
}