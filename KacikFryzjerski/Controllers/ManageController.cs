using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using KacikFryzjerski.Models;
using KacikFryzjerski.DAL;
using System.Collections.Generic;
using System.Data.Entity;

namespace KacikFryzjerski.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ProjectDbContext db;
        //private IMailService mailService;

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            Error
        }

        public ManageController(ProjectDbContext context)
        {
            this.db = context;
            //this.mailService = mailService;
        }

        public ManageController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
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

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Manage
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            var name = User.Identity.Name;

            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if (User.IsInRole("Admin"))
                ViewBag.UserIsAdmin = true;
            else
                ViewBag.UserIsAdmin = false;

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }

            var model = new ManageCredentialsViewModel
            {
                Message = message,
                AccountData = user.AccountData
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfile([Bind(Prefix = "DaneUzytkownika")]AccountData accountData)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                user.AccountData = accountData;
                var result = await UserManager.UpdateAsync(user);

                AddErrors(result);
            }

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword([Bind(Prefix = "ChangePasswordViewModel")]ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var message = ManageMessageId.ChangePasswordSuccess;
            return RedirectToAction("Index", new { Message = message });
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("password-error", error);
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        public ActionResult ListaZamowien()
        {
            var name = User.Identity.Name;

            bool isAdmin = User.IsInRole("Admin");
            ViewBag.UserIsAdmin = isAdmin;

            IEnumerable<OrderModels> orderUser;

            // Dla administratora zwracamy wszystkie zamowienia
            if (isAdmin)
            {
                orderUser = db.Orders.Include("OrderPosition").OrderByDescending(o => o.Order_ordered_at).ToArray();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                orderUser = db.Orders.Where(o => o.UserId == userId).Include("OrderPosition").OrderByDescending(o => o.Order_ordered_at).ToArray();
            }

            return View(orderUser);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public OrderStatus ZmianaStanuZamowienia(OrderModels order)
        {
            OrderModels zamowienieDoModyfikacji = db.Orders.Find(order.Id);
            zamowienieDoModyfikacji.Order_order_status = order.Order_order_status;
            db.SaveChanges();

            if (zamowienieDoModyfikacji.Order_order_status == OrderStatus.Zrealizowane)
            {
                //this.mailService.WyslanieZamowienieZrealizowaneEmail(zamowienieDoModyfikacji);
            }

            return order.Order_order_status;
        }

        /*[Authorize(Roles = "Admin")]
        public ActionResult DodajKurs(int? product_id, bool? confirmation)
        {
            ProductModels product;
            if (product_id.HasValue)
            {
                ViewBag.EditMode = true;
                product = db.Products.Find(product_id);
            }
            else
            {
                ViewBag.EditMode = false;
                product = new ProductModels();
            }

            var result = new EditKursViewModel();
            result.Kategorie = db.Kategorie.ToList();
            result.Kurs = kurs;
            result.Potwierdzenie = potwierdzenie;

            return View(result);
        }*/

        /*[HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DodajKurs(EditKursViewModel model, HttpPostedFileBase file)
        {
            if (model.Kurs.KursId > 0)
            {
                // modyfikacja kursu
                db.Entry(model.Kurs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DodajKurs", new { potwierdzenie = true });
            }
            else
            {
                // Sprawdzenie, czy użytkownik wybrał plik
                if (file != null && file.ContentLength > 0)
                {
                    if (ModelState.IsValid)
                    {
                        // Generowanie pliku
                        var fileExt = Path.GetExtension(file.FileName);
                        var filename = Guid.NewGuid() + fileExt;

                        var path = Path.Combine(Server.MapPath(AppConfig.ObrazkiFolderWzgledny), filename);
                        file.SaveAs(path);

                        model.Kurs.NazwaPlikuObrazka = filename;
                        model.Kurs.DataDodania = DateTime.Now;

                        db.Entry(model.Kurs).State = EntityState.Added;
                        db.SaveChanges();

                        return RedirectToAction("DodajKurs", new { potwierdzenie = true });
                    }
                    else
                    {
                        var kategorie = db.Kategorie.ToList();
                        model.Kategorie = kategorie;
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nie wskazano pliku");
                    var kategorie = db.Kategorie.ToList();
                    model.Kategorie = kategorie;
                    return View(model);
                }
            }

        }*/

        /*[Authorize(Roles = "Admin")]
        public ActionResult UkryjKurs(int kursId)
        {
            var kurs = db.Kursy.Find(kursId);
            kurs.Ukryty = true;
            db.SaveChanges();

            return RedirectToAction("DodajKurs", new { potwierdzenie = true });
        }*/

        /*[Authorize(Roles = "Admin")]
        public ActionResult PokazKurs(int kursId)
        {
            var kurs = db.Kursy.Find(kursId);
            kurs.Ukryty = false;
            db.SaveChanges();

            return RedirectToAction("DodajKurs", new { potwierdzenie = true });
        }*/

        /*[AllowAnonymous]
        public ActionResult WyslaniePotwierdzenieZamowieniaEmail(int zamowienieId, string nazwisko)
        {
            var zamowienie = db.Zamowienia.Include("PozycjeZamowienia").Include("PozycjeZamowienia.Kurs")
                               .SingleOrDefault(o => o.ZamowienieID == zamowienieId && o.Nazwisko == nazwisko);

            if (zamowienie == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            PotwierdzenieZamowieniaEmail email = new PotwierdzenieZamowieniaEmail();
            email.To = zamowienie.Email;
            email.From = "mariuszjurczenko@gmail.com";
            email.Wartosc = zamowienie.WartoscZamowienia;
            email.NumerZamowienia = zamowienie.ZamowienieID;
            email.PozycjeZamowienia = zamowienie.PozycjeZamowienia;
            email.sciezkaObrazka = AppConfig.ObrazkiFolderWzgledny;
            email.Send();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }*/

        /*[AllowAnonymous]
        public ActionResult WyslanieZamowienieZrealizowaneEmail(int zamowienieId, string nazwisko)
        {
            var zamowienie = db.Zamowienia.Include("PozycjeZamowienia").Include("PozycjeZamowienia.Kurs")
                                  .SingleOrDefault(o => o.ZamowienieID == zamowienieId && o.Nazwisko == nazwisko);

            if (zamowienie == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            ZamowienieZrealizowaneEmail email = new ZamowienieZrealizowaneEmail();
            email.To = zamowienie.Email;
            email.From = "mariuszjurczenko@gmail.com";
            email.NumerZamowienia = zamowienie.ZamowienieID;
            email.Send();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }*/
    }
}