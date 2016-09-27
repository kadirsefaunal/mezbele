using MEZBELE.Context;
using MEZBELE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MEZBELE.Controllers
{
    [_SessionControl]
    public class ProjectsController : Controller
    {
        private MezbeleContext db = new MezbeleContext();
        // GET: Projects
        public ActionResult Index()
        {
            Users user = db.Users.Find(Session["UserId"]);
            //List<Projects> projects = new List<Projects>();
            //foreach (var item in user.Projects)
            //{
            //    projects.Add(item);
            //}
            //List<Projects> projects = user.Projects;
            return View(user);
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