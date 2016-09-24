using System.Web.Mvc;

namespace MEZBELE.Controllers
{
    /// <summary>
    /// Karşılama sayfalarını kontrol etmekle görevli kontrolcü.
    /// </summary>
    public class LandingController : Controller
    {
        /// <summary>
        /// Anasayfa.
        /// </summary>
        /// <returns>Index isimli görünümü döndürür.</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}