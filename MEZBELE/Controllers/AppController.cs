using MEZBELE.Context;
using MEZBELE.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace MEZBELE.Controllers
{
    /// <summary>
    /// Ana uygulama kontrolcüsü.
    /// </summary>
    public class AppController : Controller
    {
        /// <summary>
        /// Veritabanı.
        /// </summary>
        private MezbeleContext db = new MezbeleContext();

        /// <summary>
        /// Uygulama anasayfası
        /// </summary>
        /// <returns>Index isimli görününmü gösterir.</returns>
        [HttpGet]
        [_SessionControl]
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                Users user = db.Users.Find(Session["UserId"]);
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Landing");
            }
        }

        /// <summary>
        /// Kullanıcı girişiyle ilgilenir.
        /// </summary>
        /// <param name="user">Giriş yapan kullanıcı.</param>
        /// <returns>Index isimli görünümü gösterir.</returns>
        [HttpPost]
        public ActionResult Index([Bind(Include = "UserName,Password")] Users user)
        {
            Users u = null;
            if (ModelState.IsValid)
            {
                bool kontrol = false;
                foreach (var item in db.Users)
                {
                    if (item.UserName == user.UserName && item.Password == user.Password)
                    {
                        u = item;
                        kontrol = true;
                        break;
                    }
                }
                if (kontrol)
                {
                    Session["UserId"] = u.Id;
                    Session["User"] = u;
                    FormsAuthentication.SetAuthCookie(u.Id.ToString(), true);
                    return View(u);
                }
                else
                    return RedirectToAction("Index", "Landing");
            }
            return View(u);
        }

        /// <summary>
        /// Kullanıcı çıkışıyla ilgilenir.
        /// </summary>
        /// <returns>Landing/Index isimli görünüme yönlendirir.</returns>
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["UserId"] = null;
            Session["User"] = null;
            return RedirectToAction("Index", "Landing");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}