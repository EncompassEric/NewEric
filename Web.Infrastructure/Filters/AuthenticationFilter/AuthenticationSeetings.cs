namespace Web.Infrastructure.Filters.AuthenticationFilter
{
    using System.Collections.Generic;

    public class AuthenticationSeetings
    {
        private static List<string> _ignoreAuthentication;
        private static List<string> _adminAuthentication; 

        public static List<string> IgnoreAuthentication
        {
            get
            {
                return _ignoreAuthentication ?? (_ignoreAuthentication = new List<string>
                    {
                        "Home;*",
                        "Register;*",
                        "Validation;*",
                        "Error;*",
                        "AdminHome;*"
                    });
            }
        }

        public static List<string> AdminAuthentication
        {
            get
            {
                return _adminAuthentication ?? (_adminAuthentication = new List<string>
                    {
                        "Task;*",
                        "Administrator;*",
                        "Backup;*",
                        "Member;*",
                        "PickCash;*",
                        "Recharge;*",
                        "Complaint;*",
                        "Financial;*",
                        "Statistics;*",
                        "SystemConfig;*"
                    });
            }
        }
    }
}
