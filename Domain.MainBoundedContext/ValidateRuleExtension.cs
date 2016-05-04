namespace Domain.MainBoundedContext
{
    public static class ValidateRuleExtension
	{
		public static bool IsNullOrEmpty(this string value)
		{
			bool result = string.IsNullOrEmpty(value);

			return result;
		}

		public static bool IsEmail(this string value)
		{
			RegexUtilities reg=new RegexUtilities();

			bool result=reg.IsValidEmail(value);

			return result;
		}

        public static bool IsPhone(this string value)
        {
            RegexUtilities reg = new RegexUtilities();

            bool result = reg.IsValidPhone(value);

            return result;
        }

		public static bool IsPositive(this int value)
		{
			bool result = value > 0;

			return result;
		}

		public static bool IsPositive(this double value)
		{
			bool result = value > 0;

			return result;
		}

		public static bool IsPositive(this decimal value)
		{
			bool result = value > 0;

			return result;
		}
	}
}
