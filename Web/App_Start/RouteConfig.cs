namespace Web.App_Start
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

		    routes.MapRoute("Default", "{controller}/{action}/{id}",
		                    new {controller = "Home", action = "Index", id = UrlParameter.Optional}, new[] {"Web.Controllers"}
		        );

		    routes.MapRoute("PageInfo", "{controller}/{action}/PageInfo/{page}",
		                    new {controller = "Home", action = "Index", page = UrlParameter.Optional}
		        );
		}
	}
}