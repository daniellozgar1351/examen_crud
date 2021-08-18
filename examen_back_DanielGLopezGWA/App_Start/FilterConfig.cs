using System.Web;
using System.Web.Mvc;

namespace examen_back_DanielGLopezGWA
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
