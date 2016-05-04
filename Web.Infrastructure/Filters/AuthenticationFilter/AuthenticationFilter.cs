namespace Web.Infrastructure.Filters.AuthenticationFilter
{
    using System.Web.Mvc;
    using Application.MainBoundedContext.Services;
    using Data.MainBoundedContext.Repositories;
    using Data.MainBoundedContext.UnitOfWork;

    public class AuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var authenticationManager = new AuthenticationManager(filterContext);

            authenticationManager.AuthenticationRequest();
        }
    }
}