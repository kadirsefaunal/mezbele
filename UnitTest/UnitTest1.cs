using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MEZBELE.Controllers;
using System.Web.Mvc;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        ///
        /// </summary>
        [TestMethod]
        public void LoginTest()
        {
            LandingController lc = new LandingController();
            var a = lc.LoginControl("321", "321") as JsonResult;
            var b = lc.LoginControl(null, null) as JsonResult;
            Assert.AreEqual(b.Data, a.Data);
        }

        /// <summary>
        ///
        /// </summary>
        [TestMethod]
        public void ProjeKaydetTest()
        {
            AppController ac = new AppController();
            var a = ac.ProjeKaydet(null) as JsonResult;
            var b = ac.ProjeKaydet(new MEZBELE.Models.Proje()) as JsonResult;
            Assert.AreEqual(b.Data, a.Data);
        }

        /// <summary>
        ///
        /// </summary>
        [TestMethod]
        public void IsEkleTest()
        {
            ProjeController pc = new ProjeController();
            var a = pc.IsEkle(null) as JsonResult;
            var b = pc.IsEkle(new MEZBELE.Models.Is()) as JsonResult;
            Assert.AreEqual(b.Data, a.Data);
        }
    }
}
