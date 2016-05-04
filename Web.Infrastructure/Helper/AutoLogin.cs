namespace Web.Infrastructure.Helper
{
    using System;
    using System.Web;
    using Application.MainBoundedContext.Services;
    using Application.Seedwork.Services;
    using Data.MainBoundedContext.Repositories;
    using Data.MainBoundedContext.UnitOfWork;
    using Domain.MainBoundedContext.UserAgg;

    public class AutoLogin
    {
        public static void SetAutoLogin(Guid userId, string sessionId)
        {
            var userIdCookie = new HttpCookie(Constants.CurrentUserId) { Value = userId.ToString(), Expires = DateTime.Now.AddDays(30) };
            HttpContext.Current.Response.Cookies.Add(userIdCookie);

            var sessionIdCookie = new HttpCookie(Constants.CurrentSessionId) { Value = sessionId, Expires = DateTime.Now.AddHours(30) };
            HttpContext.Current.Response.Cookies.Add(sessionIdCookie);
        }

        public static string GetUserIdByCookie()
        {
            var userIdCookie = HttpContext.Current.Request.Cookies[Constants.CurrentUserId];
            return userIdCookie != null ? userIdCookie.Value : null;
        }

        public static string GetSessionIdByCookie()
        {
            var sessionIdCookie = HttpContext.Current.Request.Cookies[Constants.CurrentSessionId];
            return sessionIdCookie != null ? sessionIdCookie.Value : null;
        }

        public static void DeleteAutoLogin()
        {
            var userIdCookie = HttpContext.Current.Response.Cookies[Constants.CurrentUserId];
            if (userIdCookie != null) userIdCookie.Expires = DateTime.Now.AddDays(-1);

            var sessionIdCookie = HttpContext.Current.Response.Cookies[Constants.CurrentSessionId];
            if (sessionIdCookie != null) sessionIdCookie.Expires = DateTime.Now.AddDays(-1);
        }
    }
}