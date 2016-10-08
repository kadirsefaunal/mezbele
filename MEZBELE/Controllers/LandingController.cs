using MEZBELE.Context;
using MEZBELE.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace MEZBELE.Controllers
{

    /// <summary>
    /// Karşılama sayfalarını kontrol etmekle görevli kontrolcü.
    /// </summary>
    public class LandingController : Controller
    {
        /// <summary>
        /// Veritabanı.
        /// </summary>
        private MezbeleContext db = new MezbeleContext();

        /// <summary>
        /// Kullanıcıyı, giriş yaptıysa uygulamaya, yapmadıysa karşılama sayfasına yönlendirir.
        /// </summary>
        /// <returns>Index isimli görünümü döndürür.</returns>
        public ActionResult Index()
        {
            // KULLANICI giriş kontrolü.
            if (Session["UserId"] != null)
            {
                return RedirectToAction("Index", "App");
            }

            return View();
        }

        /// <summary>
        /// Kullanıcı giriş sayfası.
        /// </summary>
        /// <returns>Login isimli görünümü gösterir.</returns>
        [HttpGet]
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Kullanıcı girişiyle ilgilenir.
        /// </summary>
        /// <param name="user">Giriş yapan kullanıcı.</param>
        /// <returns>Index isimli görünümü gösterir.</returns>
        [HttpPost]
        [Route("Login")]
        public ActionResult Login([Bind(Include = "UserName,Password")] Users user)
        {
            Users u = null;
            if (ModelState.IsValid)
            {
                bool kontrol = false;
                foreach (var item in db.Users)
                {
                    if (item.UserName == user.UserName && item.Password == user.Password)
                    {
                        u = item;
                        kontrol = true;
                        break;
                    }
                }
                if (kontrol)
                {
                    Session["UserId"] = u.Id;
                    Session["User"] = u;
                    FormsAuthentication.SetAuthCookie(u.Id.ToString(), true);

                    TempData["u"] = u;
                    return RedirectToAction("Index", "App", u);
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("Login");
        }

        /// <summary>
        /// Kullanıcı kayıt sayfası.
        /// </summary>
        /// <returns>SignUp isimli görünümü gösterir.</returns>
        [HttpGet]
        [Route("SignUp")]
        public ActionResult SignUp()
        {
            return View();
        }

        /// <summary>
        /// Kullanıcı kaydını gerçekleştirir.
        /// </summary>
        /// <param name="users">Formdan gönderilen değerler ile oluşturulan Users nesnesi.</param>
        /// <returns>Kullanıcı girişine yönlendirir.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("SignUp")]
        public ActionResult SignUp([Bind(Include = "UserName,EMail,Password")] Users users)
        {
            bool kontrol = false;
            if (ModelState.IsValid)
            {
                foreach (var item in db.Users)
                {
                    if (item.UserName == users.UserName)
                    {
                        kontrol = true;
                        break;
                    }
                }
                if (!kontrol)
                {
                    db.Users.Add(users);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }

            return RedirectToAction("Login");
        }

        /// <summary>
        /// Kullanıcı çıkışıyla ilgilenir.
        /// </summary>
        /// <returns>Index isimli görünüme yönlendirir.</returns>
        [Route("LogOut")]
        [LoginControl]
        public ActionResult LogOut()
        {
            // FormsAuthentication.SignOut(); ?????

            Session["UserId"] = null;
            Session["User"] = null;

            return RedirectToAction("Index");
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