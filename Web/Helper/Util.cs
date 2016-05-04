namespace Web.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using Controllers;
    using Domain.MainBoundedContext.MenuAgg;
    using Infrastructure.Attributes;
    using Infrastructure.Helper;
    using Action = Domain.MainBoundedContext.ActionAgg.Action;
    using Module = Domain.MainBoundedContext.ModuleAgg.Module;

    public class Util
	{
		public static List<Module> GetAllControllerModule()
		{
			List<Module> modules = new List<Module>();

			Type[] types = Assembly.GetExecutingAssembly().GetTypes();

			foreach (Type type in types)
			{
				if (type.BaseType == typeof (BaseController))
				{
					Module module = new Module();
					module.GenerateNewIdentity();
					module.ClassName = type.Name.Replace("Controller",string.Empty);
					module.NameSpace = type.Namespace;

					object[] attrs = type.GetCustomAttributes(typeof (ActionDescriptionAttribute), false);

					if (attrs.Length > 0)
					{
						string nameKey = (attrs[0] as ActionDescriptionAttribute).Description;
					}

					var methods = type.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
					methods = methods.Where(m => m.IsSpecialName == false).ToArray();

					module.Actions = new List<Action>();

					foreach (MethodInfo method in methods)
					{
						object[] postAttr = method.GetCustomAttributes(typeof (HttpPostAttribute), false);
						object[] methodAttr = method.GetCustomAttributes(typeof (ActionDescriptionAttribute), false);

						if (postAttr.Length < 1 && methodAttr.Length > 0)
						{
							Domain.MainBoundedContext.ActionAgg.Action action = new Domain.MainBoundedContext.ActionAgg.Action();
							action.Module = module;
							action.GenerateNewIdentity();
							action.MethodName = method.Name;

							module.Actions.Add(action);
						}
					}

					foreach (var method in methods)
					{
						object[] postAttr = method.GetCustomAttributes(typeof(HttpPostAttribute), false);
						//找到post的方法且没有同名的非post的方法，添加到controller的action列表里
						if (postAttr.Length > 0 && module.Actions.FirstOrDefault(x => x.MethodName.ToLower() == method.Name.ToLower()) == null)
						{
							Domain.MainBoundedContext.ActionAgg.Action action = new Domain.MainBoundedContext.ActionAgg.Action();
							action.Module = module;
							action.GenerateNewIdentity();
							action.MethodName = method.Name;

							module.Actions.Add(action);
						}
					}

					modules.Add(module);
				}
			}

			return modules;
		}

		public static Menu GetMenu(List<Module> modules, string controllerName, string actionName)
		{
			var acts = modules.Where(x => x.ClassName == controllerName).SelectMany(x => x.Actions).ToList();
			var act = acts.SingleOrDefault(x => x.MethodName == actionName);
			var rest = acts.Where(x => x.MethodName != actionName).ToList();

			Type type = Type.GetType(act.Module.NameSpace + "." + act.Module.ClassName+"Controller");

			object[] attrs = type.GetCustomAttributes(typeof (ActionDescriptionAttribute), false);

			string resourceKey = act.Module.NameSpace + "." + act.Module.ClassName + "." +
			                     (attrs[0] as ActionDescriptionAttribute).Description;
			
			Menu menu = new Menu {ActionId = act.Id, Enabled = true, IsRoot = true, NameResourceKey = resourceKey};
			menu.GenerateNewIdentity();

			menu.Children = new List<Menu>();

			foreach (var action in rest)
			{
				resourceKey = string.Empty;
				var methods = type.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
				methods = methods.Where(m => m.IsSpecialName == false).ToArray();
				foreach (MethodInfo method in methods)
				{
					if (method.Name == action.MethodName)
					{
						object[] methodAttr = method.GetCustomAttributes(typeof (ActionDescriptionAttribute), false);
						if (methodAttr.Length > 0)
						{
							resourceKey = act.Module.NameSpace + "." + act.Module.ClassName + "." +
							              (methodAttr[0] as ActionDescriptionAttribute).Description;
						}
					}
				}

				Menu child = new Menu {ActionId = action.Id, Enabled = true, NameResourceKey = resourceKey};
				child.GenerateNewIdentity();

				menu.Children.Add(child);
			}

			return menu;
		}
	}
}