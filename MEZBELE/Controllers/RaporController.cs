using MEZBELE.Models;
using MEZBELE.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace MEZBELE.Controllers
{
    /// <summary>
    /// Rapor sayfasının kontrolcüsü.
    /// </summary>
    public class RaporController : Controller
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
        /// Rapor sayfası.
        /// </summary>
        /// <param name="projeKimligi">Raporu oluşturulacak projenin kimliği</param>
        /// <returns>Rapor sayfasına yönlendirir</returns>
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