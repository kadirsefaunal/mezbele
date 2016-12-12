using MEZBELE.Context;
using MEZBELE.Models;
using System.Web.Mvc;
using System.Linq;
using System;
using System.Web;

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
            return View();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="kullaniciAdi"></param>
        /// <param name="parola"></param>
        /// <returns></returns>
        public JsonResult LoginControl(string kullaniciAdi, string parola)
        {
            int kullaniciKimligi = (from k in db.Users where k.UserName == kullaniciAdi && k.Password == parola select k.Id).SingleOrDefault();

            if (kullaniciKimligi != 0)
            {
                Response.Cookies["KullaniciKimligi"].Value = kullaniciKimligi.ToString();
                Response.Cookies["KullaniciKimligi"].Expires = DateTime.Now.AddDays(1);

                HttpCookie sonZiyaret = new HttpCookie("SonZiyaret", DateTime.Now.ToString());
                sonZiyaret.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(sonZiyaret);

                return Json("Giriş başarılı.");
            }
            else
            {
                return Json("Giriş başarısız.");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [Route("Register")]
        public ActionResult Register()
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