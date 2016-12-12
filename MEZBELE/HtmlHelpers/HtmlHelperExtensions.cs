using System.Web.Mvc;

namespace MEZBELE.HtmlHelpers
{
    public static class HtmlHelperExtensions
    {
        public static string ActivePage(this HtmlHelper helper, string action, string controller)
        {
            string sinifDegeri = "";

            string currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
            string currentAction = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();

            if (currentController == controller && currentAction == action)
            {
                sinifDegeri = "active";
            }

            return sinifDegeri;
        }
    }
}