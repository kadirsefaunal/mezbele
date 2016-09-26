using MEZBELE.Context;
using MEZBELE.Models;
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
        /// Kullanıcıyı, giriş yaptıysa uygulamaya, yapmadıysa karşılama sayfasına yönlendirir.
        /// </summary>
        /// <returns>Index isimli görünümü döndürür.</returns>
        public ActionResult Index()
        {
            // KULLANICI giriş kontrolü.
            //if (Session["UserId"] != null)
            //{
            //    return RedirectToAction("Index", "App");
            //}

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
            bool kontrol = false;
            if (ModelState.IsValid)
            {
                foreach (var item in db.Users)
                {
                    if (item.UserName == users.UserName)
                    {
                        kontrol = true;
                        break;
                    }
                }
                if (!kontrol)
                {
                    db.Users.Add(users);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Users");
                }
            }

            return RedirectToAction("Index");
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