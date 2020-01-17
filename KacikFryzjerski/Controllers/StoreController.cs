using KacikFryzjerski.DAL;
using KacikFryzjerski.Infrastructure;
using KacikFryzjerski.Models;
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
        private readonly ProjectDbContext db = new ProjectDbContext();

        public ActionResult CategoryList(string category_id, string searchQuery = null)
        {
            if (Request.IsAjaxRequest())
            {
                var products = db.Products.OrderByDescending(x => x.Id)
                .Where(x => (searchQuery == null ||
                x.Product_name.ToLower().Contains(searchQuery.ToLower())));

                return PartialView("_CategoryList", products);
            }
            else if (category_id == "All" || category_id == null)
            {
                var products = db.Products.OrderByDescending(x => x.Id).ToList();
                return View(products);
            }
            else
            {
                var products = db.Products.OrderByDescending(x => x.Id).Where(x => x.Product_category_id.ToString() == category_id).ToList();
                return View(products);
            }
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60000)]
        public ActionResult CategoryMenu()
        {
            ICacheProvider cache = new DefaultCacheProvider();
            List<CategoryModels> categories;

            if (cache.IsSet(Consts.CategoriesCacheKey))
            {
                categories = cache.Get(Consts.CategoriesCacheKey) as List<CategoryModels>;
            }
            else
            {
                categories = db.Categories.OrderByDescending(x => x.Category_name).ToList();
                cache.Set(Consts.CategoriesCacheKey, categories, 60);
            }

            return PartialView("_CategoryMenu", categories);
        }

        public ActionResult Detail(int product_id)
        {
            var product = db.Products.Find(product_id);
            return View(product);
        }

        public ActionResult ProductHint(string term)
        {
            var products = db.Products.Where(x => x.Product_name.ToLower().Contains(term.ToLower())).Take(5)
                .Select(x => new { label = x.Product_name });

            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
}