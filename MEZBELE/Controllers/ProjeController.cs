using MEZBELE.Models;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using MEZBELE.ViewModels;
using System;
using System.Net;

namespace MEZBELE.Controllers
{
    public class ProjeController : Controller
    {
        /// <summary>
        ///
        /// </summary>
        private readonly MezbeleDBEntities db = new MezbeleDBEntities();
        /// <summary>
        ///
        /// </summary>
        private VM vm;

        /// <summary>
        ///
        /// </summary>
        /// <param name="projeKimligi"></param>
        /// <returns></returns>
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
        ///
        /// </summary>
        /// <param name="projeKimligi"></param>
        /// <returns></returns>
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
        ///
        /// </summary>
        /// <param name="surecKimligi"></param>
        /// <returns></returns>
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
        ///
        /// </summary>
        /// <param name="projeKimligi"></param>
        /// <param name="surecKimligi"></param>
        /// <param name="isKimligi"></param>
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

                return View(vm);
            }
            return RedirectToAction("Index", "Landing");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="surec"></param>
        /// <returns></returns>
        public JsonResult SurecKaydet(Surec surec)
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

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public JsonResult KullanicilariGetir()
        {
            int kullaniciKimligi = Convert.ToInt32(Request.Cookies["KullaniciKimligi"].Value);
            var kullanicilar = (from k in db.Kullanici
                                where k.ID != kullaniciKimligi
                                select new { Adi = k.Adi, Soyadi = k.Soyadi, ID = k.ID, Avatar = k.Avatar }).ToList();

            return Json(kullanicilar);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="kpr"></param>
        /// <returns></returns>
        public JsonResult ProjeyeAta(KullaniciProjeRol kpr)
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="eklenecekIs"></param>
        /// <returns></returns>
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

                bolum = (tamamlanan / surec.Is.Count()) * 100;
                surec.TamamlanmaOrani = Convert.ToInt32(bolum);

                db.SaveChanges();

                return Json("Başarılı.");
            }
            catch (Exception)
            {
                return Json("Başarısız!");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="surecKimligi"></param>
        /// <param name="eklenecekNot"></param>
        /// <returns></returns>
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
            catch (Exception)
            {
                return Json("Başarısız!");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="projeKimligi"></param>
        /// <param name="kullaniciKimligi"></param>
        /// <returns></returns>
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
            catch (Exception)
            {
                return Json("Başarısız!");
            }
        }
    }
}