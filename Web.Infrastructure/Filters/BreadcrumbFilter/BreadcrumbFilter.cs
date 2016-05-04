namespace Web.Infrastructure.Filters.BreadcrumbFilter
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class BreadcrumbFilter : ActionFilterAttribute
	{
		private IBreadcrumbManager _breadcrumbManager = null;

		public BreadcrumbFilter()
		{
			_breadcrumbManager=new DefaultBreadcrumbManager();
		}
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var controller = (Controller)filterContext.Controller;
			
			IList<Breadcrumb> breadcrumbs=_breadcrumbManager.PushBreadcrumb(filterContext);

			controller.TempData["breadcrumb"] = breadcrumbs;
		}
	}
}
