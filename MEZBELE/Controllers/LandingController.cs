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
        private readonly MezbeleDBEntities db = new MezbeleDBEntities();

        /// <summary>
        /// Kullanıcıyı, giriş yaptıysa uygulamaya, yapmadıysa karşılama sayfasına yönlendirir.
        /// </summary>
        /// <returns>Index isimli görünümü döndürür.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Giriş sayfası.
        /// </summary>
        /// <returns>Kullanıcı giriş yapmış ise uygulamaya, yapmamışsa giriş sayfasına yönlendirir.</returns>
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
        /// Kullanıcı girişini kontrol eder.
        /// </summary>
        /// <param name="kullaniciAdi">Kullanıcı Adı</param>
        /// <param name="parola">Kullanıcı Parolası</param>
        /// <returns>Sistemde böyle bir kullanıcı varsa uygulama sayfasına yönlendirir.</returns>
        public JsonResult LoginControl(string kullaniciAdi, string parola)
        {
            try
            {
                int kullaniciKimligi = (from k in db.Kullanici where k.KullaniciAdi == kullaniciAdi && k.Parola == parola select k.ID).SingleOrDefault();

                if (kullaniciKimligi != 0)
                {
                    Response.Cookies["KullaniciKimligi"].Value = kullaniciKimligi.ToString();
                    Response.Cookies["KullaniciKimligi"].Expires = DateTime.Now.AddDays(1);

                    var kullanici = (from k in db.Kullanici where k.ID == kullaniciKimligi select k).SingleOrDefault();
                    Log log = new Log()
                    {
                        Kullanici = kullanici,
                        Tarih = DateTime.Now,
                        Aciklama = "Sisteme giriş yaptı."
                    };
                    db.Log.Add(log);
                    db.SaveChanges();

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
            catch
            {
                return Json("Giriş başarısız.");
            }
        }

        /// <summary>
        /// Kayıt sayfası.
        /// </summary>
        /// <returns>Sistemde giriş yapmış kullanıcı yok ise kayıt sayfasına yönlendirir.</returns>
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
        /// Kullanıcı kaydını kontrol eder.
        /// </summary>
        /// <param name="kullaniciAdi">Kullanıcı Adı</param>
        /// <param name="parola">Kullanıcı Parolası</param>
        /// <param name="isim">Kullanıcı İsmi</param>
        /// <param name="soyisim">Kullanıcı Soyismi</param>
        /// <param name="eposta">Kullanıcı Epostası</param>
        /// <returns>Kayıt işlem durumunu gönderir.</returns>
        public JsonResult RegisterControl(string kullaniciAdi, string parola, string isim, string soyisim, string eposta)
        {
            try
            {
                var kontrol = (from k in db.Kullanici where k.KullaniciAdi == kullaniciAdi select k).SingleOrDefault();

                if (kontrol == null)
                {
                    Kullanici kayitEdilecekKullanici = new Kullanici
                    {
                        KullaniciAdi = kullaniciAdi,
                        Parola = parola,
                        Adi = isim,
                        Soyadi = soyisim,
                        EMail = eposta
                    };

                    db.Kullanici.Add(kayitEdilecekKullanici);
                    db.SaveChanges();
                    return Json("Kayıt başarılı.");
                }
                else
                {
                    return Json("Kayıt başarısız.");
                }
            }
            catch
            {
                return Json("Kayıt başarısız.");
            }
        }

        /// <summary>
        /// Kullanıcıyı sistemden çıkartır.
        /// </summary>
        /// <returns>Çıkış durumunu gönderir.</returns>
        public JsonResult Logout()
        {
            try
            {
                int kullaniciID = int.Parse(Request.Cookies["KullaniciKimligi"].Value);
                var kullanici = (from k in db.Kullanici where k.ID == kullaniciID select k).SingleOrDefault();
                Log log = new Log()
                {
                    Kullanici = kullanici,
                    Tarih = DateTime.Now,
                    Aciklama = "Sistemden çıkış yaptı."
                };
                db.Log.Add(log);
                db.SaveChanges();

                Response.Cookies["KullaniciKimligi"].Value = null;
                Response.Cookies["KullaniciKimligi"].Expires = DateTime.Now.AddDays(-1);

                HttpCookie sonZiyaret = new HttpCookie("SonZiyaret", DateTime.Now.ToString());
                sonZiyaret.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(sonZiyaret);

                return Json("Çıkış yapıldı.");
            }
            catch
            {
                return Json("Çıkış yapılamadı.");
            }
        }

        /// <summary>
        /// Standart db dispose metodu.
        /// </summary>
        /// <param name="disposing">Dispose durumu</param>
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