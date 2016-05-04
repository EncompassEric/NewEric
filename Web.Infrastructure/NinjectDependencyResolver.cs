namespace Web.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using Application.Seedwork.Services;
    using System.Web.Mvc;
    using Domain.MainBoundedContext.MenuAgg;
    using Domain.MainBoundedContext.ModuleAgg;
    using Ninject;
    using Application.MainBoundedContext.Services;
    using Data.MainBoundedContext.Repositories;

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();

            AddBindings();
        }


        private void AddBindings()
        {
            _kernel.Bind<IMenuRepository>().To<MenuRepository>();
            _kernel.Bind<IMenuService>().To<MenuService>();

            _kernel.Bind<IModuleRepository>().To<ModuleRepository>();
            _kernel.Bind<IModuleService>().To<ModuleService>();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}
