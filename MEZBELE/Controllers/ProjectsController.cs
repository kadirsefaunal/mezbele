using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MEZBELE.Context;
using MEZBELE.Models;
using System;

namespace MEZBELE.Controllers
{
    /// <summary>
    /// Projeler ile ilgilenen kontrolcü.
    /// </summary>
    [_SessionControl]
    public class ProjectsController : Controller
    {
        private MezbeleContext db = new MezbeleContext();

        // GET: Projects
        public ActionResult Index()
        {
            Users user = db.Users.Find(Session["UserId"]);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projects projects = db.Projects.Find(id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            return View(projects);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IsPrivate,Name,Description")] Projects projects)
        {
            if (ModelState.IsValid)
            {
                Users user = db.Users.Find(Session["UserId"]);
                projects.OwnerID = user.Id;
                projects.IsIndividual = true;
                projects.IsPrivate = true;
                projects.CreationDate = DateTime.Now;
                projects.ChangeDate = DateTime.Now;
                db.Projects.Add(projects);
                user.Projects.Add(projects);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projects);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projects projects = db.Projects.Find(id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            return View(projects);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IsIndividual,IsPrivate,Name,Description")] Projects projects)
        {
            if (ModelState.IsValid)
            {
                projects.ChangeDate = DateTime.Now;
                db.Entry(projects).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projects);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projects projects = db.Projects.Find(id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            return View(projects);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Projects projects = db.Projects.Find(id);
            db.Projects.Remove(projects);
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
