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
                return View(vm);
            }
            return RedirectToAction("Index", "Landing");
        }
    }
}