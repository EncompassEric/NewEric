
namespace Domain.MainBoundedContext.ModuleAgg
{
    using System.Collections.Generic;
    using ActionAgg;
    using Domain.Seedwork;

    public class Module:Entity
	{
		public string ClassName { get; set; }

		public virtual List<Action> Actions { get; set; }

		public string NameSpace { get; set; }
	}
}
