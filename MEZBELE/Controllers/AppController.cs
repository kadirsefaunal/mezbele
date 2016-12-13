using MEZBELE.Models;
using System.Web.Mvc;
using System.Linq;
using System;

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
        private readonly MezbeleEntities db = new MezbeleEntities();
        
        /// <summary>
        /// Uygulama anasayfası
        /// </summary>
        /// <returns>Index isimli görününmü gösterir.</returns>
        public ActionResult Index()
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                int kullaniciID = Convert.ToInt32(Request.Cookies["KullaniciKimligi"].Value);
                var kullanici = (from k in db.Users
                                 where k.ID == kullaniciID
                                 select k).SingleOrDefault();
                return View(kullanici);
            }
            return RedirectToAction("Index", "Landing");
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public new ActionResult Profile()
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                int kullaniciID = Convert.ToInt32(Request.Cookies["KullaniciKimligi"].Value);
                var kullanici = (from k in db.Users
                                 where k.ID == kullaniciID
                                 select k).SingleOrDefault();
                return View(kullanici);
            }
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