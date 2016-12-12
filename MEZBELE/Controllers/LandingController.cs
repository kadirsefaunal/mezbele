using MEZBELE.Context;
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
        private readonly MezbeleContext db = new MezbeleContext();

        /// <summary>
        /// Kullanıcıyı, giriş yaptıysa uygulamaya, yapmadıysa karşılama sayfasına yönlendirir.
        /// </summary>
        /// <returns>Index isimli görünümü döndürür.</returns>
        public ActionResult Index()
        {
            return View();
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