using MEZBELE.Models;
using MEZBELE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MEZBELE.Controllers
{
    public class RaporController : Controller
    {
        /// <summary>
        /// Veritabanı.
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
        public ActionResult Index(int projeKimligi)
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                vm = new VM();

                vm.AktifProje = (from p in db.Proje
                                 where p.ID == projeKimligi
                                 select p).SingleOrDefault();

                vm.Yonetici = (from y in db.Kullanici
                               where y.ID == vm.AktifProje.YoneticiID
                               select y).SingleOrDefault();

                vm.Musteri = (from m in db.Kullanici
                              where m.ID == vm.AktifProje.ProjeSahibiID
                              select m).SingleOrDefault();

                return View(vm);
            }
            return RedirectToAction("Index", "Landing");
        }
    }
}