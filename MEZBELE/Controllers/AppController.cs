using MEZBELE.Models;
using System.Web.Mvc;

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
                return View();
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
                return View();
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