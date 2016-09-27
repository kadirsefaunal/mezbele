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

        #region Giriş Sistemi çalışması
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Landing");
        }

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

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["UserId"] = null;
            return RedirectToAction("Index", "Landing");
        }

        #endregion

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