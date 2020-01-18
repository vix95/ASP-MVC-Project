using KacikFryzjerski.DAL;
using KacikFryzjerski.Infrastructure;
using KacikFryzjerski.Models;
using KacikFryzjerski.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KacikFryzjerski.Controllers
{
    public class CartController : Controller
    {
        private CartManager cartManager;
        private ISessionManager sessionManager { get; set; }
        private DbContext db;

        public CartController()
        {
            db = new DbContext();
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

        /*public async Task<ActionResult> Pay()
        {
            var name = User.Identity.Name;

            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var userOwnTable = db.Users.Where(x => x.User_email == user.Email).SingleOrDefault();
                var order = new OrderModels
                {
                    Order_name = userOwnTable.User_AccountData.Name,
                    Order_surname = userOwnTable.User_AccountData.Surname,
                    Order_address = userOwnTable.User_AccountData.Address,
                    Order_city = userOwnTable.User_AccountData.City,
                    Order_postcode = userOwnTable.User_AccountData.Postcode,
                    Order_email = userOwnTable.User_AccountData.Email,
                    Order_phone = userOwnTable.User_AccountData.Phone
                };
                return View(order);
            }
            else
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Pay", "Cart") });
        }*/

        /*[HttpPost]
        public async Task<ActionResult> Pay(OrderModels orderDetails)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var newOrder = cartManager.CreateOrder(orderDetails, userId);
                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.AccountData);
                await UserManager.UpdateAsync(user);

                cartManager.EmptyCart();

                //maileService.WyslaniePotwierdzenieZamowieniaEmail(newOrder);

                return RedirectToAction("OrderConfirmation");
            }
            else
                return View(orderDetails);
        }*/

        public ActionResult OrderConfirmation()
        {
            var name = User.Identity.Name;
            return View();
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}