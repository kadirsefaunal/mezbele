using System.Web.Mvc;

namespace MEZBELE.HtmlHelpers
{
    /// <summary>
    /// Yardımcı eklentiler.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Aktif sayfaya göre dinamik menü sınıf atar.
        /// </summary>
        /// <param name="helper">Bu yardımcı</param>
        /// <param name="action">Aktif kontrolcü metodu</param>
        /// <param name="controller">Aktif kontrolcü sınıf</param>
        /// <returns>Sınıf değeri döndürür</returns>
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