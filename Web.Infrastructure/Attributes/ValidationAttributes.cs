
namespace Web.Infrastructure.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using Domain.MainBoundedContext;

    public class ValidationAttributes : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            //邮箱可以为空
            if (value == null)
            {
                return true;
            }

            return value is string && ((string) value).IsEmail();
        }
    }

    public class PhoneAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is string && ((string)value).IsPhone();
        }
    }
}
