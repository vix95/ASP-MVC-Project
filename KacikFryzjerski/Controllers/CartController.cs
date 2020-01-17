using KacikFryzjerski.DAL;
using KacikFryzjerski.Infrastructure;
using KacikFryzjerski.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KacikFryzjerski.Controllers
{
    public class CartController : Controller
    {
        private CartManager cartManager;
        private ISessionManager sessionManager { get; set; }
        private ProjectDbContext db;

        public CartController()
        {
            db = new ProjectDbContext();
            sessionManager = new SessionManager();
            cartManager = new CartManager(sessionManager, db);
        }

        public ActionResult Index()
        {
            var cartPositions = cartManager.GetCart();
            var cartTotalPrice = cartManager.GetCartValue();
            CartViewModel cartViewModel = new CartViewModel()
            {
                cartPositions = cartPositions,
                TotalPrice = cartTotalPrice
            };

            return View(cartViewModel);
        }

        public ActionResult AddToCart(int product_id)
        {
            cartManager.AddToCart(product_id);

            return RedirectToAction("Index");
        }

        public int GetCartElemenetsQuantity()
        {
            return cartManager.GetQuantityCartPosition();
        }

        public ActionResult DeleteFromCart(int product_id)
        {
            int positionsQuantity = cartManager.DeleteFromCart(product_id);
            int positionsQuantityCart = cartManager.GetQuantityCartPosition();
            double cartValue = cartManager.GetCartValue();

            var result = new CartDeletingViewModel()
            {
                PositionIdToDelete = product_id,
                PositionsQuantityToDelete = positionsQuantity,
                CartTotalValue = cartValue,
                CartPositionsQuantity = positionsQuantityCart
            };

            return Json(result);
        }
    }
}