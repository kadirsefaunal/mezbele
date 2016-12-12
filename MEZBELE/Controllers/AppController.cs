using MEZBELE.Context;
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
        private readonly MezbeleContext db = new MezbeleContext();

        /// <summary>
        /// Uygulama anasayfası
        /// </summary>
        /// <returns>Index isimli görününmü gösterir.</returns>
        public ActionResult Index()
        {
            Users user = db.Users.Find(Session["UserId"]);

            return View(user);
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