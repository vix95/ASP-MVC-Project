using KacikFryzjerski.DAL;
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
            var bestsellers = db.items.OrderBy(x => Guid.NewGuid()).Take(6).ToList();
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

        public ActionResult ItemDetails(string id)
        {

            return View();
        }
    }
}