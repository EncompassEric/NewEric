namespace Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;

    [HandleError]
    public class ErrorController : Controller
    {
        public ActionResult Error()
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View();
        }

        public ActionResult NotFound()
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View();
        }
    }
}
