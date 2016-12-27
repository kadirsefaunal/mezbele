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

        /// <summary>
        /// ViewModel.
        /// </summary>
        private VM vm;

        /// <summary>
        /// Uygulama anasayfası.
        /// </summary>
        /// <returns>Sistemde kullanıcı var ise anasayfaya yoksa karşılama sayfasına yönlendirir</returns>
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
                               select p.Proje).Distinct().OrderByDescending(t => t.OlusturmaTarihi).ToList();
                vm.Isler = (from ik in db.IsKullanici
                            where ik.KullaniciID == kullaniciID && ik.Is.AktifMi == true
                            select ik.Is).ToList();
                return View(vm);
            }
            return RedirectToAction("Index", "Landing");
        }

        /// <summary>
        /// Kullanıcı profil sayfası.
        /// </summary>
        /// <returns>Sistemde kullanıcı varsa profil sayfasına yoksa karşılama sayfasına yönlendirir</returns>
        public ActionResult Profil()
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
        /// Projeler sayfası.
        /// </summary>
        /// <returns>Sistemde kullanıcı varsa projeler sayfasına yoksa karşılama sayfasına yönlendirir</returns>
        public ActionResult Projeler()
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                int kullaniciID = int.Parse(Request.Cookies["KullaniciKimligi"].Value);
                vm = new VM();
                vm.Kullanici = (from k in db.Kullanici
                                where k.ID == kullaniciID
                                select k).SingleOrDefault();
                vm.Projeler = (from p in db.Proje
                               orderby p.OlusturmaTarihi descending
                               select p).ToList();
                return View(vm);
            }
            return RedirectToAction("Index", "Landing");
        }

        /// <summary>
        /// ProjeEkle sayfası.
        /// </summary>
        /// <returns>Sistemde kullanıcı varsa proje ekleme sayfasına yoksa karşılama sayfasına yönlendirir</returns>
        public ActionResult ProjeEkle()
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
        /// Projeyi sisteme kaydeder.
        /// </summary>
        /// <param name="proje">Kaydedilecek proje.</param>
        /// <returns>Proje kaydetme durumunu döndürür</returns>
        public JsonResult ProjeKaydet(Proje proje)
        {
            try
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
                return Json("Ekleme başarılı.");
            }
            catch
            {
                return Json("Ekleme başarısız.");
            }
        }

        /// <summary>
        /// Proje yönetici adaylarını getirir.
        /// </summary>
        /// <returns>Yönetici adaylarını döndürür</returns>
        public JsonResult YoneticiAdaylariniGetir()
        {
            var kullanicilar = (from k in db.Kullanici
                                select new { Adi = k.Adi, Soyadi = k.Soyadi, ID = k.ID, Avatar = k.Avatar }).ToList();
            return Json(kullanicilar);
        }

        /// <summary>
        /// Kullanıcı bilgilerini günceller.
        /// </summary>
        /// <param name="isim">Yeni kullanıcı ismi</param>
        /// <param name="soyisim">Yeni kullanıcı soyismi</param>
        /// <param name="eposta">Yeni kullanıcı epostası</param>
        /// <param name="parola">Yeni kullanıcı parolası</param>
        /// <param name="webLink">Yeni kullanıcı web bağlantısı</param>
        /// <param name="avatarLink">Yeni kullanıcı resim bağlantısı</param>
        /// <returns>Güncelleme durumunu döndürür</returns>
        public JsonResult UpdateUserInfo(string isim, string soyisim, string eposta,
                                         string parola, string webLink, string avatarLink)
        {
            try
            {
                if (Request.Cookies["KullaniciKimligi"] != null)
                {
                    int kullaniciID = int.Parse(Request.Cookies["KullaniciKimligi"].Value);
                    var kullanici = (from k in db.Kullanici where k.ID == kullaniciID select k).SingleOrDefault();

                    if (kullanici != null)
                    {
                        kullanici.Adi = isim;
                        kullanici.Soyadi = soyisim;
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
            catch
            {
                return Json("Güncelleme başarısız.");
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