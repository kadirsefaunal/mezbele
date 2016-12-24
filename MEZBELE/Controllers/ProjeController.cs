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
                return View(vm);
            }
            return RedirectToAction("Index", "Landing");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projeID"></param>
        /// <returns></returns>
        public ActionResult SurecEkle(int projeID)
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                vm = new VM();
                int kullaniciKimligi = Convert.ToInt32(Request.Cookies["KullaniciKimligi"].Value);
                vm.Kullanici = (from k in db.Kullanici
                                where k.ID == kullaniciKimligi
                                select k).SingleOrDefault();
                vm.AktifProje = (from p in db.Proje
                                 where p.ID == projeID
                                 select p).SingleOrDefault();

                if (vm.Kullanici.ID == vm.AktifProje.YoneticiID)
                    return View(vm);
                else
                    return RedirectToAction("Index", "App");
                
            }
            return RedirectToAction("Index", "Landing");
        }

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
    }
}