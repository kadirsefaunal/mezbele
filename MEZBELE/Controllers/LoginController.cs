using System.Web;
using System.Web.Mvc;

namespace MEZBELE.Controllers
{
    public class LoginControlAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["User"] == null || HttpContext.Current.Session["UserId"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/Landing/Index");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}