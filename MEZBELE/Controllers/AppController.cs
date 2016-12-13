using MEZBELE.Models;
using System.Web.Mvc;
using System.Linq;
using System;
using System.Data.Entity;

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
        private readonly MezbeleEntities db = new MezbeleEntities();

        /// <summary>
        /// Uygulama anasayfası.
        /// </summary>
        /// <returns>Index isimli görününmü gösterir.</returns>
        public ActionResult Index()
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                int kullaniciID = int.Parse(Request.Cookies["KullaniciKimligi"].Value);
                ViewBag.Kullanici = (from k in db.Users
                                     where k.ID == kullaniciID
                                     select k).SingleOrDefault();
                return View();
            }
            return RedirectToAction("Index", "Landing");
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public new ActionResult Profile()
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                int kullaniciID = int.Parse(Request.Cookies["KullaniciKimligi"].Value);
                ViewBag.Kullanici = (from k in db.Users
                                     where k.ID == kullaniciID
                                     select k).SingleOrDefault();
                return View();
            }
            return RedirectToAction("Index", "Landing");
        }

        public JsonResult UpdateUserInfo(string isim, string soyisim, string eposta,
                                         string parola, string webLink, string konum,
                                         string bolge, string avatarLink)
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                int kullaniciID = int.Parse(Request.Cookies["KullaniciKimligi"].Value);
                var kullanici = (from k in db.Users where k.ID == kullaniciID select k).SingleOrDefault();

                if (kullanici != null)
                {
                    kullanici.FirstName = isim;
                    kullanici.LastName = soyisim;
                    kullanici.EMail = eposta;
                    kullanici.Password = parola;
                    kullanici.WebAdresi = webLink;
                    kullanici.Konum = konum;
                    kullanici.Bolge = bolge;
                    kullanici.UserAvatar = avatarLink;

                    var girdi = db.Entry(kullanici);

                    girdi.State = EntityState.Modified;
                    girdi.Property(k => k.UserName).IsModified = false;
                    girdi.Property(k => k.RoleID).IsModified = false;
                    girdi.Property(k => k.SonGiris).IsModified = false;

                    db.SaveChanges();

                    return Json("Güncelleme başarılı.");
                }

                return Json("Güncelleme başarısız.");
            }

            return Json("Güncelleme başarısız.");
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