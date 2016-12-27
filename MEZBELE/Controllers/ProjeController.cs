using MEZBELE.Models;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using MEZBELE.ViewModels;
using System;
using System.Net;

namespace MEZBELE.Controllers
{
    /// <summary>
    /// Proje sisteminin kontrolcüsü.
    /// </summary>
    public class ProjeController : Controller
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
        /// Proje detay sayfası.
        /// </summary>
        /// <param name="projeKimligi">Detayları gösterilecek projenin kimliği</param>
        /// <returns>Sistemde kullanıcı varsa proje detay sayfasına yoksa karşılama sayfasına yönlendirir</returns>
        public ActionResult Detay(int projeKimligi)
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                if (projeKimligi == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                int kullaniciID = int.Parse(Request.Cookies["KullaniciKimligi"].Value);

                vm = new VM();

                vm.Kullanici = (from k in db.Kullanici
                                where k.ID == kullaniciID
                                select k).SingleOrDefault();

                vm.AktifProje = (from p in db.Proje
                                 where p.ID == projeKimligi
                                 select p).SingleOrDefault();

                vm.Yonetici = (from y in db.Kullanici
                               where y.ID == vm.AktifProje.YoneticiID
                               select y).SingleOrDefault();

                vm.Calisanlar = (from c in db.Kullanici
                                 join k in db.KullaniciProjeRol on c.ID equals k.KullaniciID
                                 where k.ProjeID == vm.AktifProje.ID
                                 select c).Distinct().ToList();

                vm.Roller = (from r in db.Rol
                             where r.RolAdi != "Müşteri" && r.RolAdi != "Yönetici"
                             select r).ToList();

                return View(vm);
            }
            return RedirectToAction("Index", "Landing");
        }

        /// <summary>
        /// Süreç ekle sayfası.
        /// </summary>
        /// <param name="projeKimligi">Süreç eklenecek projenin kimliği</param>
        /// <param name="anaSurecKimligi">Süreç alt süreç olacaksa ana sürecinin kimliği</param>
        /// <returns>Sistemde kullanıcı varsa süreç ekleme sayfasına yoksa karşılama sayfasına yönlendirir</returns>
        public ActionResult SurecEkle(int projeKimligi, int anaSurecKimligi)
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                vm = new VM();

                int kullaniciKimligi = Convert.ToInt32(Request.Cookies["KullaniciKimligi"].Value);

                vm.Kullanici = (from k in db.Kullanici
                                where k.ID == kullaniciKimligi
                                select k).SingleOrDefault();

                vm.AktifProje = (from p in db.Proje
                                 where p.ID == projeKimligi
                                 select p).SingleOrDefault();

                if (anaSurecKimligi != -1)
                {
                    vm.AktifSurec = (from s in db.Surec
                                     where s.ID == anaSurecKimligi
                                     select s).SingleOrDefault();
                }

                if (vm.Kullanici.ID == vm.AktifProje.YoneticiID)
                    return View(vm);
                else
                    return RedirectToAction("Index", "App");
            }
            return RedirectToAction("Index", "Landing");
        }

        /// <summary>
        /// Süreç detay sayfası.
        /// </summary>
        /// <param name="projeKimligi">Sürecin ait olduğu projenin kimliği</param>
        /// <param name="surecKimligi">Detayları gösterilecek sürecin kimliği</param>
        /// <returns>Sistemde kullanıcı varsa süreç detay sayfasına yoksa karşılama sayfasına yönlendirir</returns>
        public ActionResult SurecDetay(int projeKimligi, int surecKimligi)
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                vm = new VM();

                int kullaniciKimligi = Convert.ToInt32(Request.Cookies["KullaniciKimligi"].Value);

                vm.Kullanici = (from k in db.Kullanici
                                where k.ID == kullaniciKimligi
                                select k).SingleOrDefault();

                vm.AktifSurec = (from s in db.Surec
                                 where s.ID == surecKimligi
                                 select s).SingleOrDefault();

                vm.AktifProje = (from p in db.Proje
                                 where p.ID == projeKimligi
                                 select p).SingleOrDefault();

                return View(vm);
            }
            return RedirectToAction("Index", "Landing");
        }

        /// <summary>
        /// İş detay sayfası.
        /// </summary>
        /// <param name="projeKimligi">İşin ait olduğu projenin kimliği</param>
        /// <param name="surecKimligi">İşin ait olduğu sürecin kimliği</param>
        /// <param name="isKimligi">Detayları gösterilecek işin kimliği</param>
        /// <returns></returns>
        public ActionResult IsDetay(int projeKimligi, int surecKimligi, int isKimligi)
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                vm = new VM();

                int kullaniciKimligi = Convert.ToInt32(Request.Cookies["KullaniciKimligi"].Value);

                vm.Kullanici = (from k in db.Kullanici
                                where k.ID == kullaniciKimligi
                                select k).SingleOrDefault();

                vm.AktifSurec = (from s in db.Surec
                                 where s.ID == surecKimligi
                                 select s).SingleOrDefault();

                vm.AktifProje = (from p in db.Proje
                                 where p.ID == projeKimligi
                                 select p).SingleOrDefault();

                vm.AktifIs = (from i in db.Is
                              where i.ID == isKimligi
                              select i).SingleOrDefault();

                vm.AtananKisi = (from a in db.IsKullanici
                                 where a.IsID == isKimligi
                                 select a).SingleOrDefault();

                return View(vm);
            }
            return RedirectToAction("Index", "Landing");
        }

        /// <summary>
        /// Süreci kaydeder.
        /// </summary>
        /// <param name="surec">Kaydedilecek süreç</param>
        /// <returns>Kaydetme durumunu gönderir</returns>
        public JsonResult SurecKaydet(Surec surec)
        {
            try
            {
                int kullaniciKimligi = Convert.ToInt32(Request.Cookies["KullaniciKimligi"].Value);
                TimeSpan Sure = Convert.ToDateTime(surec.BitisTarihi) - Convert.ToDateTime(surec.BaslamaTarihi);
                surec.OlusturanKullaniciID = kullaniciKimligi;
                surec.Sure = Convert.ToInt32(Sure.TotalDays);
                surec.TamamlanmaOrani = 0;

                db.Surec.Add(surec);
                db.SaveChanges();

                return Json("başarılı");
            }
            catch
            {
                return Json("başarısız");
            }
        }

        /// <summary>
        /// Sistemdeki diğer kullanıcıları getirir.
        /// </summary>
        /// <returns>Kullanıcıları döndürür</returns>
        public JsonResult KullanicilariGetir()
        {
            try
            {
                int kullaniciKimligi = Convert.ToInt32(Request.Cookies["KullaniciKimligi"].Value);
                var kullanicilar = (from k in db.Kullanici
                                    where k.ID != kullaniciKimligi
                                    select new { Adi = k.Adi, Soyadi = k.Soyadi, ID = k.ID, Avatar = k.Avatar }).ToList();

                return Json(kullanicilar);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Projeye kullanıcı atar.
        /// </summary>
        /// <param name="kpr">Eklenecek rol</param>
        /// <returns>Kayıt durumunu döndürür</returns>
        public JsonResult ProjeyeAta(KullaniciProjeRol kpr)
        {
            try
            {
                var rolDurum = (from r in db.KullaniciProjeRol
                                where r.KullaniciID == kpr.KullaniciID && r.ProjeID == kpr.ProjeID && r.RolID == kpr.RolID
                                select r).SingleOrDefault();
                if (rolDurum == null)
                {
                    db.KullaniciProjeRol.Add(kpr);
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
        /// İş ekler.
        /// </summary>
        /// <param name="eklenecekIs">Eklenecek iş</param>
        /// <returns>Ekleme durumunu döndürür</returns>
        public JsonResult IsEkle(Is eklenecekIs)
        {
            try
            {
                int kullaniciKimligi = Convert.ToInt32(Request.Cookies["KullaniciKimligi"].Value);

                eklenecekIs.OlusturanID = kullaniciKimligi;
                eklenecekIs.OlusturmaTarihi = DateTime.Now;

                db.Is.Add(eklenecekIs);

                var surec = (from s in db.Surec
                             where s.ID == eklenecekIs.SurecID
                             select s).SingleOrDefault();

                float tamamlanan = 0, bolum = 0;

                foreach (var item in surec.Is)
                {
                    if (item.AktifMi == false)
                        tamamlanan++;
                }

                bolum = (tamamlanan / surec.Is.Count) * 100;
                surec.TamamlanmaOrani = Convert.ToInt32(bolum);

                db.SaveChanges();

                return Json("Başarılı.");
            }
            catch
            {
                return Json("Başarısız!");
            }
        }

        /// <summary>
        /// Sürece not ekler.
        /// </summary>
        /// <param name="surecKimligi">Not eklenecek sürecin kimliği</param>
        /// <param name="eklenecekNot">Eklenecek not</param>
        /// <returns>Ekleme durumunu döndürür</returns>
        public JsonResult SureceNotEkle(int surecKimligi, string eklenecekNot)
        {
            try
            {
                var surec = (from s in db.Surec
                             where s.ID == surecKimligi
                             select s).SingleOrDefault();

                surec.Notlar = eklenecekNot;

                db.SaveChanges();

                return Json("Başarılı.");
            }
            catch
            {
                return Json("Başarısız!");
            }
        }

        /// <summary>
        /// Kullanıcıyı projeden çıkartır.
        /// </summary>
        /// <param name="projeKimligi">Kullanıcının dahil olduğu projenin kimliği</param>
        /// <param name="kullaniciKimligi">Projeden çıkartılacak kullanıcının kimliği</param>
        /// <returns>Çıkarma durumunu döndürür</returns>
        public JsonResult ProjedenCikar(int projeKimligi, int kullaniciKimligi)
        {
            try
            {
                var kprListe = (from kpr in db.KullaniciProjeRol
                                where kpr.KullaniciID == kullaniciKimligi && kpr.ProjeID == projeKimligi
                                select kpr).ToList();

                var kAtananIsler = (from ik in db.IsKullanici
                                    where ik.KullaniciID == kullaniciKimligi && ik.Is.Surec.ProjeID == projeKimligi
                                    select ik).ToList();

                foreach (var kpr in kprListe)
                {
                    db.KullaniciProjeRol.Remove(kpr);
                }

                foreach (var kai in kAtananIsler)
                {
                    db.IsKullanici.Remove(kai);
                }

                db.SaveChanges();

                return Json("Çıkarma başarılı.");
            }
            catch
            {
                return Json("Çıkarma başarısız.");
            }
        }

        /// <summary>
        /// Proje bilgilerini günceller.
        /// </summary>
        /// <param name="proje">Güncel proje bilgileri</param>
        /// <returns>Güncelleme durumunu döndürür</returns>
        public JsonResult ProjeGuncelle(Proje proje)
        {
            try
            {
                var guncellenecekProje = (from p in db.Proje
                                          where p.ID == proje.ID
                                          select p).SingleOrDefault();

                guncellenecekProje.ProjeAdi = proje.ProjeAdi;
                guncellenecekProje.Butce = proje.Butce;
                guncellenecekProje.Aciklama = proje.Aciklama;
                guncellenecekProje.DegistirmeTarihi = DateTime.Now;

                db.SaveChanges();

                return Json("Başarılı.");
            }
            catch
            {
                return Json("Başarısız!");
            }
        }

        /// <summary>
        /// Süreç bilgilerini günceller.
        /// </summary>
        /// <param name="surec">Güncel süreç bilgileri</param>
        /// <returns>Güncelleme durumunu döndürür</returns>
        public JsonResult SurecGuncelle(Surec surec)
        {
            try
            {
                var guncellenecekSurec = (from s in db.Surec
                                          where s.ID == surec.ID
                                          select s).SingleOrDefault();

                guncellenecekSurec.SurecAdi = surec.SurecAdi;
                guncellenecekSurec.Oncelik = surec.Oncelik;
                guncellenecekSurec.Aciklama = surec.SurecAdi;

                db.SaveChanges();

                return Json("Başarılı.");
            }
            catch
            {
                return Json("Başarısız!");
            }
        }

        /// <summary>
        /// Projeyi sistemden siler.
        /// </summary>
        /// <param name="projeKimligi">Silinecek projenin kimliği</param>
        /// <returns>Silme durumunu döndürür</returns>
        public JsonResult ProjeSil(int projeKimligi)
        {
            try
            {
                var kprList = (from k in db.KullaniciProjeRol
                               where k.ProjeID == projeKimligi
                               select k).ToList();

                foreach (var kpr in kprList)
                {
                    db.KullaniciProjeRol.Remove(kpr);
                }

                var ikListesi = (from ik in db.IsKullanici
                                 where ik.Is.Surec.ProjeID == projeKimligi
                                 select ik).ToList();

                foreach (var ik in ikListesi)
                {
                    db.IsKullanici.Remove(ik);
                }

                var isListesi = (from i in db.Is
                                 where i.Surec.ProjeID == projeKimligi
                                 select i).ToList();

                foreach (var i in isListesi)
                {
                    db.Is.Remove(i);
                }

                var surecListesi = (from s in db.Surec
                                    where s.ProjeID == projeKimligi
                                    select s).ToList();

                foreach (var surec in surecListesi)
                {
                    db.Surec.Remove(surec);
                }

                var proje = (from p in db.Proje
                             where p.ID == projeKimligi
                             select p).SingleOrDefault();

                db.Proje.Remove(proje);

                db.SaveChanges();

                return Json("Başarılı.");
            }
            catch
            {
                return Json("Başarısız!");
            }
        }

        /// <summary>
        /// Süreci projeden siler.
        /// </summary>
        /// <param name="surecKimlik">Silinecek sürecin kimliği</param>
        /// <returns>Silme durumunu döndürür</returns>
        public JsonResult SureciSil(int surecKimlik)
        {
            try
            {
                var isKullaniciListesi = (from ik in db.IsKullanici
                                          where ik.Is.SurecID == surecKimlik
                                          select ik).ToList();

                foreach (var isK in isKullaniciListesi)
                {
                    db.IsKullanici.Remove(isK);
                }

                var isListesi = (from i in db.Is
                                 where i.SurecID == surecKimlik
                                 select i).ToList();

                foreach (var i in isListesi)
                {
                    db.Is.Remove(i);
                }

                var altSurecler = (from s in db.Surec
                                   where s.AnaSurecID == surecKimlik
                                   select s).ToList();

                foreach (var altSurec in altSurecler)
                {
                    db.Surec.Remove(altSurec);
                }

                var surec = (from s in db.Surec
                             where s.ID == surecKimlik
                             select s).SingleOrDefault();

                db.Surec.Remove(surec);

                db.SaveChanges();

                return Json("Başarılı.");
            }
            catch
            {
                return Json("Başarısız.");
            }
        }

        /// <summary>
        /// İşi süreçten siler.
        /// </summary>
        /// <param name="isKimligi">Silinecek işin kimliği</param>
        /// <returns>Silme durumunu döndürür</returns>
        public JsonResult IsiSil(int isKimligi)
        {
            try
            {
                var surec = (from i in db.Is
                             where i.ID == isKimligi
                             select i.Surec).SingleOrDefault();

                var isKullanici = (from ik in db.IsKullanici
                                   where ik.IsID == isKimligi
                                   select ik).ToList();

                foreach (var ik in isKullanici)
                {
                    db.IsKullanici.Remove(ik);
                }

                var silinecekIs = (from i in db.Is
                                   where i.ID == isKimligi
                                   select i).SingleOrDefault();

                db.Is.Remove(silinecekIs);

                db.SaveChanges();

                float tamamlanan = 0, bolum = 0;

                foreach (var item in surec.Is)
                {
                    if (item.AktifMi == false)
                        tamamlanan++;
                }

                bolum = (tamamlanan / surec.Is.Count) * 100;
                surec.TamamlanmaOrani = Convert.ToInt32(bolum);

                db.SaveChanges();

                return Json("Başarılı.");
            }
            catch
            {
                return Json("Başarısız!");
            }
        }

        /// <summary>
        /// İşi kulllanıcıya atar.
        /// </summary>
        /// <param name="ik">Atama ilişki bilgisi</param>
        /// <returns>Atama durumunu döndürür</returns>
        public JsonResult IsiAta(IsKullanici ik)
        {
            try
            {
                db.IsKullanici.Add(ik);
                db.SaveChanges();

                return Json("Başarılı.");
            }
            catch
            {
                return Json("Başarısız!");
            }
        }

        /// <summary>
        /// Sistemdeki kullanıcıyı işe atar.
        /// </summary>
        /// <param name="isKimligi">Atanılacak işin kimliği</param>
        /// <returns>Atama durumunu döndürür</returns>
        public JsonResult IsiAl(int isKimligi)
        {
            try
            {
                int kullaniciKimligi = Convert.ToInt32(Request.Cookies["KullaniciKimligi"].Value);

                IsKullanici ik = new IsKullanici()
                {
                    IsID = isKimligi,
                    KullaniciID = kullaniciKimligi
                };

                db.IsKullanici.Add(ik);
                db.SaveChanges();

                return Json("Başarılı.");
            }
            catch
            {
                return Json("Başarısız!");
            }
        }

        /// <summary>
        /// Sistemdeki kullanıcıyı işten çıkartır.
        /// </summary>
        /// <param name="isKimligi">Çıkılacak işin kimliği</param>
        /// <returns>Çıkma durumunu döndürür</returns>
        public JsonResult IsiBirak(int isKimligi)
        {
            try
            {
                int kullaniciKimligi = Convert.ToInt32(Request.Cookies["KullaniciKimligi"].Value);
                var isKullanici = (from ik in db.IsKullanici
                                   where ik.IsID == isKimligi && ik.KullaniciID == kullaniciKimligi
                                   select ik).SingleOrDefault();

                db.IsKullanici.Remove(isKullanici);
                db.SaveChanges();

                return Json("Başarılı.");
            }
            catch
            {
                return Json("Başarısız!");
            }
        }

        /// <summary>
        /// Projeye dahil olan kullanıcıları getirir.
        /// </summary>
        /// <param name="projeKimligi">Projenin kimliği</param>
        /// <returns>Projede çalışanları döndürür</returns>
        public JsonResult ProjedekiKullanicilariGetir(int projeKimligi)
        {
            try
            {
                int kullaniciKimligi = Convert.ToInt32(Request.Cookies["KullaniciKimligi"].Value);
                var kullanicilar = (from kpr in db.KullaniciProjeRol
                                    join k in db.Kullanici on kpr.KullaniciID equals k.ID
                                    where kpr.ProjeID == projeKimligi && kpr.KullaniciID != kullaniciKimligi
                                    select new { Adi = k.Adi, Soyadi = k.Soyadi, ID = k.ID, Avatar = k.Avatar }).Distinct().ToList();

                return Json(kullanicilar);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// İşi tamamlandı olarak işaretler.
        /// </summary>
        /// <param name="isKimligi">İşaretlenecek işin kimliği</param>
        /// <returns>İşaretleme durumunu döndürür</returns>
        public JsonResult IsiTamamla(int isKimligi)
        {
            try
            {
                var surec = (from i in db.Is
                             where i.ID == isKimligi
                             select i.Surec).SingleOrDefault();

                var guncellenecekIs = (from i in db.Is
                                       where i.ID == isKimligi
                                       select i).SingleOrDefault();

                guncellenecekIs.AktifMi = false;
                guncellenecekIs.BitisTarihi = DateTime.Now;

                db.SaveChanges();

                float tamamlanan = 0, bolum = 0;

                foreach (var item in surec.Is)
                {
                    if (item.AktifMi == false)
                        tamamlanan++;
                }

                bolum = (tamamlanan / surec.Is.Count) * 100;
                surec.TamamlanmaOrani = Convert.ToInt32(bolum);

                db.SaveChanges();

                return Json("Başarılı.");
            }
            catch
            {
                return Json("Başarısız!");
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