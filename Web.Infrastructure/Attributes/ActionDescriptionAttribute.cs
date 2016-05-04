namespace Web.Infrastructure.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
	public class ActionDescriptionAttribute : Attribute
	{
		public string Description { get; set; }

		public ActionDescriptionAttribute(string description)
		{
			this.Description = description;
		}
	}
}
