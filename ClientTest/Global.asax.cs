using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LicenseCommon;

namespace ClientTest
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			LicenseFile.FilePath = HttpContext.Current.Server.MapPath("~/") + "lisans.lic";
		}
	}
}
