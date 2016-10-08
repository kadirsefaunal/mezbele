using System.Linq;
using System.Net;
using System.Web.Mvc;
using MEZBELE.Context;
using MEZBELE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace MEZBELE.Controllers
{
    /// <summary>
    /// Projeler ile ilgilenen kontrolcü.
    /// </summary>
    [LoginControl]
    public class ProjectsController : Controller
    {
        /// <summary>
        /// Veritabanı.
        /// </summary>
        private MezbeleContext db = new MezbeleContext();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Users user = db.Users.Find(Session["UserId"]);
            if (user == null)
            {
                return HttpNotFound();
            }

            List<Projects> projects = user.Projects;
            foreach (var crew in user.Crews)
            {
                foreach (var project in crew.Projects)
                {
                    if (!projects.Contains(project))
                    {
                        projects.Add(project);
                    }
                }
            }
            return View(projects);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Projects projects = db.Projects.Find(id);
            db.Projects.Remove(projects);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        /// <summary>
        /// Projeye ekip atamak için kullanılır.
        /// </summary>
        /// <returns>AddCrew isimli görünümü gösterir.</returns>
        [Route("Projects/{id?}/AddCrew")]
        public ActionResult AddCrew(int? id)
        {
            Projects project = db.Projects.Find(id);
            var AllCrews = db.Crews.ToList();

            foreach (var crew in db.Crews.ToList())
            {
                if (project.Crews.Contains(crew) || Session["UserId"].ToString() != crew.OwnerId.ToString())
                {
                    AllCrews.Remove(crew);
                }
            }

            var CrewList = AllCrews.Where(u => u.Id != 0).ToList().Select(u => new
            {
                Id = u.Id,
                Ad = string.Format("#{0}: {1}", u.Id, u.Name)
            });
            
            ViewData["AllCrews"] = new SelectList(
                CrewList,
                "Id",
                "Ad",
                CrewList.First()
            );
            return View(project);
        }

        /// <summary>
        /// Projeye ekibi ekler.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="crewId">Eklenecek ekip kimliği.</param>
        /// <returns>Projects/Index isimli görünüme yönlendirir.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Projects/{id?}/AddCrew")]
        public ActionResult AddCrew(int id, string crewId)
        {
            Projects project = db.Projects.Find(id);
            Crews crew = db.Crews.Find(int.Parse(crewId));

            project.Crews.Add(crew);
            crew.Projects.Add(project);

            db.Entry(crew).State = EntityState.Modified;
            db.Entry(project).State = EntityState.Modified;

            db.SaveChanges();

            return RedirectToAction("Details", new { id = project.Id });
        }

        /// <summary>
        /// Projeden ekibi çıkartır.
        /// </summary>
        /// <param name="id">Proje kimliği.</param>
        /// <param name="crewId">Çıkartılacak ekibin kimliği.</param>
        /// <returns>Projects/Index isimli görünüme yönlendirir.</returns>
        [Route("Projects/{id?}/DeleteCrew/{crewId?}")]
        public ActionResult DeleteCrew(int id, int crewId)
        {
            Projects project = db.Projects.Find(id);
            Crews crew = db.Crews.Find(crewId);
            
                project.Crews.Remove(crew);
                crew.Projects.Remove(project);

                db.Entry(crew).State = EntityState.Modified;
                db.Entry(project).State = EntityState.Modified;

                db.SaveChanges();
            

            return RedirectToAction("Details", new { id = project.Id });
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
