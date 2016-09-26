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
        private MezbeleContext db = new MezbeleContext();

        /// <summary>
        /// Uygulama anasayfasının kontrolcüsü.
        /// </summary>
        /// <returns>Uygulama anasayfasını döndürür.</returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Kullanıcı girişi yapar.
        /// </summary>
        /// <param name="users">Formdan gönderilen değerler ile oluşturulan Users nesnesi.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UserName,Password")] Users users)
        {
            bool kontrol = false;
            int userId = -1;
            if (ModelState.IsValid)
            {
                foreach (var item in db.Users)
                {
                    if (item.UserName == users.UserName && item.Password == users.Password)
                    {
                        userId = item.Id;
                        kontrol = true;
                        break;
                    }
                }
                if (kontrol && userId != -1)
                {
                    Session["UserId"] = userId;
                    Session["UserName"] = users.UserName;
                    return View(users);
                    //return RedirectToAction("Index", "App");
                }
                else
                    return RedirectToAction("Index", "Landing");
            }


            return View(users);
        }
    }
}