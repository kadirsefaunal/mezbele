using MEZBELE.Context;
using MEZBELE.Models;
using System.Linq;
using System.Web.Mvc;

namespace MEZBELE.Controllers
{
    /// <summary>
    /// Ana uygulama kontrolcüsü.
    /// </summary>
    public class AppController : Controller
    {
        private MezbeleContext db = new MezbeleContext();

        /// <summary>
        /// Kullanıcı girişi yapar.
        /// </summary>
        /// <param name="users">Formdan gönderilen değerler ile oluşturulan Users nesnesi.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UserName,Password")] Users users)
        {
            Users user = null;
            bool kontrol = false;
            if (ModelState.IsValid)
            {
                foreach (var item in db.Users)
                {
                    if (item.UserName == users.UserName && item.Password == users.Password)
                    {
                        user = item;
                        kontrol = true;
                        break;
                    }
                }
                if (kontrol)
                {
                    Session["UserId"] = user.Id;
                    Session["UserName"] = user.UserName;
                    return View(user);
                }
                else
                    return RedirectToAction("Index", "Landing");
            }
            
            return View(user);
        }

        /// <summary>
        /// Kullanıcının çıkış yapmasını sağlar.
        /// </summary>
        public ActionResult LogOut()
        {
            if (Session["UserId"] != null && Session["UserName"] != null)
            {
                Session["UserId"] = null;
                Session["UserName"] = null;
            }
            return RedirectToAction("Index", "Landing");
        }

        /// <summary>
        /// DB dispose metodu.
        /// </summary>
        /// <param name="disposing">Durum belirteci.</param>
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