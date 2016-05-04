using System.Collections.Generic;

namespace Web.Infrastructure.Helper
{
    using System;
    using Domain.MainBoundedContext.UserAgg;

    public class StaticObjects
    {
        public static string CurrentUserId
        {
            get
            {
                var userId = System.Web.HttpContext.Current.Session[Constants.CurrentUserId];
                return userId != null ? ((Guid)userId).ToString() : null;
            }
        }

        public static User CurrentUser
        {
            get
            {
                var userId = System.Web.HttpContext.Current.Session[Constants.CurrentUserId];
                return null;
            }
            set
            {
                if (value != null)
                {
                    System.Web.HttpContext.Current.Session[Constants.CurrentUserId] = value.Id;
                }
                else
                {
                    System.Web.HttpContext.Current.Session[Constants.CurrentUserId] = null;
                }
            }
        }

        public static string CurrentSessionId
        {
            get
            {
                return System.Web.HttpContext.Current.Session.SessionID;
            }
        }
    }
}
