namespace Web.Infrastructure.Filters.BreadcrumbFilter
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public interface IBreadcrumbManager
	{
		IList<Breadcrumb> PushBreadcrumb(ActionExecutingContext context);
	}
}
