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
        private readonly MezbeleEntities db = new MezbeleEntities();

        /// <summary>
        /// Kullanıcıyı, giriş yaptıysa uygulamaya, yapmadıysa karşılama sayfasına yönlendirir.
        /// </summary>
        /// <returns>Index isimli görünümü döndürür.</returns>
        public ActionResult Index()
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                return RedirectToAction("Index", "App");
            }
            return View();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [Route("Login")]
        public ActionResult Login()
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                return RedirectToAction("Index", "App");
            }
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
            int kullaniciKimligi = (from k in db.Users where k.UserName == kullaniciAdi && k.Password == parola select k.ID).SingleOrDefault();

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
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                return RedirectToAction("Index", "App");
            }
            return View();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="kullaniciAdi"></param>
        /// <param name="parola"></param>
        /// <returns></returns>
        public JsonResult RegisterControl(string kullaniciAdi, string parola, string isim, string soyisim, string eposta)
        {
            var kontrol = (from k in db.Users where k.UserName == kullaniciAdi select k).SingleOrDefault();

            if (kontrol == null)
            {
                User kayitEdilecekKullanici = new User
                {
                    UserName = kullaniciAdi,
                    Password = parola,
                    FirstName = isim,
                    LastName = soyisim,
                    EMail = eposta,
                    RoleID = 2
                };

                db.Users.Add(kayitEdilecekKullanici);
                db.SaveChanges();
                return Json("Kayıt başarılı.");
            }
            else
            {
                return Json("Kayıt başarısız.");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public JsonResult Logout()
        {
            Response.Cookies["KullaniciKimligi"].Value = null;
            Response.Cookies["KullaniciKimligi"].Expires = DateTime.Now.AddDays(-1);

            HttpCookie sonZiyaret = new HttpCookie("SonZiyaret", DateTime.Now.ToString());
            sonZiyaret.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(sonZiyaret);

            return Json("Çıkış yapıldı.");
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