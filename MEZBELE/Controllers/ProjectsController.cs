using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MEZBELE.Context;
using MEZBELE.Models;
using System;
using System.Collections.Generic;

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
        public ActionResult Create([Bind(Include = "Id,IsPrivate,Name,Description")] Projects project)
        {
            if (ModelState.IsValid)
            {
                Users user = db.Users.Find(Session["UserId"]);
                project.Users = new List<Users>();
                project.Crews = new List<Crews>();
                project.Issues = new List<Issues>();
                project.OwnerID = user.Id;
                project.IsIndividual = true;
                project.IsPrivate = true;
                project.CreationDate = DateTime.Now;
                project.ChangeDate = DateTime.Now;
                project.Users.Add(user);
                user.Projects.Add(project);
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
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
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Projects project)
        {
            if (ModelState.IsValid)
            {
                project.ChangeDate = DateTime.Now;
                db.Projects.Attach(project);
                db.Entry(project).Property(e => e.Name).IsModified = true;
                db.Entry(project).Property(e => e.Description).IsModified = true;
                db.Entry(project).Property(e => e.ChangeDate).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
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
