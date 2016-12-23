using MEZBELE.Models;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using MEZBELE.ViewModels;
using System;

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
        private readonly MezbeleDBEntities db = new MezbeleDBEntities();
        private VM vm;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                int kullaniciID = int.Parse(Request.Cookies["KullaniciKimligi"].Value);
                vm = new VM();
                vm.Kullanici = (from k in db.Kullanici
                                where k.ID == kullaniciID
                                select k).SingleOrDefault();
                vm.Projeler = (from p in db.KullaniciProjeRol
                               where p.KullaniciID == kullaniciID
                               select p.Proje).ToList();
                return View(vm);
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
                vm = new VM();
                vm.Kullanici = (from k in db.Kullanici
                                where k.ID == kullaniciID
                                select k).SingleOrDefault();
                return View(vm);
            }
            return RedirectToAction("Index", "Landing");
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ActionResult Projects()
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                int kullaniciID = int.Parse(Request.Cookies["KullaniciKimligi"].Value);
                vm = new VM();
                vm.Kullanici = (from k in db.Kullanici
                                where k.ID == kullaniciID
                                select k).SingleOrDefault();
                vm.Projeler = (from p in db.Proje
                               where p.ProjeSahibiID == kullaniciID
                               select p).ToList();
                return View(vm);
            }
            return RedirectToAction("Index", "Landing");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjeEkle()
        {
            if(Request.Cookies["KullaniciKimligi"] != null)
            {
                int kullaniciID = int.Parse(Request.Cookies["KullaniciKimligi"].Value);
                vm = new VM();
                vm.Kullanici = (from k in db.Kullanici
                                where k.ID == kullaniciID
                                select k).SingleOrDefault();
                return View(vm);
            }
            return RedirectToAction("Index", "Landing");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proje"></param>
        /// <param name="yonetici"></param>
        /// <returns></returns>
        public JsonResult ProjeKaydet(Proje proje)
        {
            proje.OlusturmaTarihi = DateTime.Now;
            proje.AktifMi = true;
            db.Proje.Add(proje);

            var sahip = (from s in db.Kullanici
                         where s.ID == proje.ProjeSahibiID
                         select s).SingleOrDefault();
            var yonetici = (from y in db.Kullanici
                            where y.ID == proje.YoneticiID
                            select y).SingleOrDefault();
            var yoneticiRol = (from r in db.Rol
                               where r.RolAdi == "Yönetici"
                               select r).SingleOrDefault();

            KullaniciProjeRol kpr = new KullaniciProjeRol()
            {
                Kullanici = yonetici,
                Proje = proje,
                Rol = yoneticiRol
            };
            db.KullaniciProjeRol.Add(kpr);

            if (proje.ProjeSahibiID != proje.YoneticiID)
            {
                var musteriRol = (from r in db.Rol
                                  where r.RolAdi == "Müşteri"
                                  select r).SingleOrDefault();
                KullaniciProjeRol kpr2 = new KullaniciProjeRol()
                {
                    Kullanici = sahip,
                    Proje = proje,
                    Rol = musteriRol
                };
                db.KullaniciProjeRol.Add(kpr2);
            }
            
            db.SaveChanges();
            return Json("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult KullanicilariGetir(string a)
        {
            var kullanicilar = (from k in db.Kullanici
                                select new { Adi = k.Adi, Soyadi = k.Soyadi, ID = k.ID }).ToList();
            return Json(kullanicilar);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="isim"></param>
        /// <param name="soyisim"></param>
        /// <param name="eposta"></param>
        /// <param name="parola"></param>
        /// <param name="webLink"></param>
        /// <param name="konum"></param>
        /// <param name="bolge"></param>
        /// <param name="avatarLink"></param>
        /// <returns></returns>
        public JsonResult UpdateUserInfo(string isim, string soyisim, string eposta,
                                         string parola, string webLink, string konum,
                                         string bolge, string avatarLink)
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                int kullaniciID = int.Parse(Request.Cookies["KullaniciKimligi"].Value);
                var kullanici = (from k in db.Kullanici where k.ID == kullaniciID select k).SingleOrDefault();

                if (kullanici != null)
                {
                    kullanici.Adi = isim;
                    kullanici.Soyadi= soyisim;
                    kullanici.EMail = eposta;
                    kullanici.Parola = parola;
                    kullanici.WebAdresi = webLink;
                    kullanici.Avatar = avatarLink;

                    var girdi = db.Entry(kullanici);

                    girdi.State = EntityState.Modified;
                    girdi.Property(k => k.KullaniciAdi).IsModified = false;
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