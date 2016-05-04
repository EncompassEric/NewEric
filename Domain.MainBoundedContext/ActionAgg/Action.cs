
namespace Domain.MainBoundedContext.ActionAgg
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using Domain.Seedwork;
    using ModuleAgg;

    public class Action:Entity
	{
		public string MethodName { get; set; }

		public Guid ModuleId { get; set; }

		public virtual Module Module { get; set; }

		public string Parameter { get; set; }

		[NotMapped]
		public string Url
		{
			get { return "/"+Module.ClassName + "/" + MethodName; }
		}
	}
}
