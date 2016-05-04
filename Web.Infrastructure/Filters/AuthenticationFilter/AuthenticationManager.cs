namespace Web.Infrastructure.Filters.AuthenticationFilter
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Application.Seedwork.Services;
    using Helper;
    using System.Linq;

    public class AuthenticationManager
    {
        private readonly ActionExecutingContext _context;

        public Type ControllerType { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public AuthenticationManager(ActionExecutingContext context)
        {
            _context = context;

            ControllerType = _context.ActionDescriptor.ControllerDescriptor.ControllerType;
            ControllerName = _context.ActionDescriptor.ControllerDescriptor.ControllerName;
            ActionName = _context.ActionDescriptor.ActionName;
        }

        public void AuthenticationRequest()
        {
            if (!ControllerType.FullName.Contains("Web.Areas.Admin.Controllers"))
            {
                
                _context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Register", Action = "SignIn" }));
            }
            else
            {
                _context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "AdminHome", Action = "AdminHome" }));
            }
        }
    }
}
