using MEZBELE.Context;
using MEZBELE.Models;
using System.Net;
using System.Web.Mvc;

namespace MEZBELE.Controllers
{

    /// <summary>
    /// Karşılama sayfalarını kontrol etmekle görevli kontrolcü.
    /// </summary>
    public class LandingController : Controller
    {
        /// <summary>
        /// Veritabanı.
        /// </summary>
        private MezbeleContext db = new MezbeleContext();
        /// <summary>
        /// Anasayfa.
        /// </summary>
        /// <returns>Index isimli görünümü döndürür.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Kullanıcı kaydını gerçekleştirir.
        /// </summary>
        /// <param name="users">Formdan gönderilen değerler ile oluşturulan Users nesnesi.</param>
        /// <returns>Kullanıcı listesine yönlendirir.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UserName,EMail,Password")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index", "Users");
            }

            return RedirectToAction("Index");
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