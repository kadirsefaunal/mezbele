using System.Web.Mvc;

namespace MEZBELE.Controllers
{
    /// <summary>
    /// Ana uygulama kontrolcüsü.
    /// </summary>
    public class AppController : Controller
    {
        /// <summary>
        /// Uygulama anasayfasının kontrolcüsü.
        /// </summary>
        /// <returns>Uygulama anasayfasını döndürür.</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}