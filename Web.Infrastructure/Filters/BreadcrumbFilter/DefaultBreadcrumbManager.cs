namespace Web.Infrastructure.Filters.BreadcrumbFilter
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class DefaultBreadcrumbManager:IBreadcrumbManager
	{
		public IList<Breadcrumb> PushBreadcrumb(ActionExecutingContext context)
		{
			IList<Breadcrumb> breadcrumbs = new List<Breadcrumb>();

			UrlHelper helper = new UrlHelper(context.RequestContext);
			breadcrumbs.Add(new Breadcrumb
				{
					Text = context.ActionDescriptor.ControllerDescriptor.ControllerName,
					Url = helper.Action("Index")
				});
			breadcrumbs.Add(new Breadcrumb
			{
				Text = context.ActionDescriptor.ActionName,
				Url = string.Empty
			});

			return breadcrumbs;
		}

	}
}
