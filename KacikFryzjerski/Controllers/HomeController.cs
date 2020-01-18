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
    public class HomeController : Controller
    {
        private readonly DbContext db = new DbContext();
        public ActionResult Index()
        {
            ICacheProvider cache = new DefaultCacheProvider();
            List<ProductModels> bestsellers;

            if (cache.IsSet(Consts.BestsellersCacheKey))
            {
                bestsellers = cache.Get(Consts.BestsellersCacheKey) as List<ProductModels>;
            }
            else
            {
                bestsellers = db.Products.Where(x => x.Product_is_bestseller == true).OrderBy(x => Guid.NewGuid()).Take(3).ToList();
                cache.Set(Consts.BestsellersCacheKey, bestsellers, 60);
            }

            var home_view_model = new HomeViewModel()
            {
                Bestsellers = bestsellers
            };

            return View(home_view_model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}