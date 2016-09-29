using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MEZBELE.Context;
using MEZBELE.Models;
using System.Collections.Generic;

namespace MEZBELE.Controllers
{
    [_SessionControl]
    public class CrewsController : Controller
    {
        private MezbeleContext db = new MezbeleContext();

        /// <summary>
        /// Ekip listesini gösterir.
        /// </summary>
        /// <returns>Index isimli görünümü gösterir.</returns>
        public ActionResult Index()
        {
            Users user = db.Users.Find(Session["UserId"]);
            return View(user.Crews.ToList());
        }

        /// <summary>
        /// Ekip detay görünümü.
        /// </summary>
        /// <param name="id">Detayları gösterilecek ekip kimliği.</param>
        /// <returns>Details isimli görünümü gösterir.</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crews crew = db.Crews.Find(id);
            if (crew == null)
            {
                return HttpNotFound();
            }
            return View(crew);
        }

        /// <summary>
        /// Ekip oluşturma görünümü.
        /// </summary>
        /// <returns>Edit isimli görünümü gösterir.</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Girilen bilgiler ile yeni bir ekip oluşturur.
        /// </summary>
        /// <param name="crew">Sisteme eklenecek ekip.</param>
        /// <returns>Ekip paneli anasayfasına yönlendirir.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Avatar")] Crews crew)
        {
            if (ModelState.IsValid)
            {
                Users user = db.Users.Find(Session["UserId"]);
                
                crew.Users = new List<Users>();
                crew.Projects = new List<Projects>();

                crew.OwnerId = user.Id;
                user.Crews.Add(crew);
                crew.Users.Add(user);

                db.Crews.Add(crew);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(crew);
        }

        /// <summary>
        /// Ekip bilgilerini düzenleme görünümü.
        /// </summary>
        /// <param name="id">Düzenlenecek ekibin kimliği.</param>
        /// <returns>Ekip paneli anasayfasına yönlendirir.</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crews crew = db.Crews.Find(id);
            if (crew == null)
            {
                return HttpNotFound();
            }
            return View(crew);
        }

        /// <summary>
        /// Ekip bilgilerindeki değişiklikleri sisteme kaydeder.
        /// </summary>
        /// <param name="crew">Değişiklik yapılan ekip.</param>
        /// <returns>Ekip paneli anasayfasına yönlendirir.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CrewName,CrewAvatar")] Crews crew)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crew).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(crew);
        }

        /// <summary>
        /// Ekip silme işlemi.
        /// </summary>
        /// <param name="id">Silinecek ekibin kimliği.</param>
        /// <returns>Ekip paneli anasayfasına yönlendirir.</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crews crew = db.Crews.Find(id);
            if (crew == null)
            {
                return HttpNotFound();
            }
            return View(crew);
        }

        /// <summary>
        /// Silme işlemini onaylar.
        /// </summary>
        /// <param name="id">Silinecek ekibin kimliği.</param>
        /// <returns>Ekip paneli anasayfasına yönlendirir.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Crews crew = db.Crews.Find(id);
            Users user = db.Users.Find(Session["UserId"]);

            db.Crews.Remove(crew);
            user.Crews.Remove(crew);

            db.SaveChanges();
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
