using System.Web;
using System.Web.Mvc;

namespace SistemaSolicitudesSoporte
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
