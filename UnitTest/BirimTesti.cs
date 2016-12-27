using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MEZBELE.Controllers;
using System.Web.Mvc;

namespace BirimTesti
{
    [TestClass]
    public class BirimTesti
    {
        /// <summary>
        /// İki kez yanlış girdi yollayarak test eder, sonuçları eşitse test başarılıdır.
        /// </summary>
        [TestMethod]
        public void GirisTesti()
        {
            LandingController lc = new LandingController();
            var a = lc.LoginControl("321", "321") as JsonResult;
            var b = lc.LoginControl(null, null) as JsonResult;
            Assert.AreEqual(b.Data, a.Data);
        }

        /// <summary>
        /// İki kez yanlış girdi yollayarak test eder, sonuçları eşitse test başarılıdır.
        /// </summary>
        [TestMethod]
        public void ProjeKaydetTesti()
        {
            AppController ac = new AppController();
            var a = ac.ProjeKaydet(null) as JsonResult;
            var b = ac.ProjeKaydet(new MEZBELE.Models.Proje()) as JsonResult;
            Assert.AreEqual(b.Data, a.Data);
        }

        /// <summary>
        /// İki kez yanlış girdi yollayarak test eder, sonuçları eşitse test başarılıdır.
        /// </summary>
        [TestMethod]
        public void IsEkleTesti()
        {
            ProjeController pc = new ProjeController();
            var a = pc.IsEkle(null) as JsonResult;
            var b = pc.IsEkle(new MEZBELE.Models.Is()) as JsonResult;
            Assert.AreEqual(b.Data, a.Data);
        }
    }
}
