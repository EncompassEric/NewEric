
namespace Data.MainBoundedContext.UnitOfWork
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Domain.MainBoundedContext.ActionAgg;
    using Domain.MainBoundedContext.ModuleAgg;
    using Domain.MainBoundedContext.MenuAgg;
    using Domain.MainBoundedContext.PrivilegeAgg;
    using Domain.MainBoundedContext.RoleAgg;
    using Domain.MainBoundedContext.UserAgg;
    using Mapping;

    public partial class MainBCUnitOfWork
    {
        private IDbSet<User> _users;
        public IDbSet<User> Users
        {
            get { return _users ?? (_users = base.Set<User>()); }
        }

        private IDbSet<Role> _roles;
        public IDbSet<Role> Roles
        {
            get { return _roles ?? (_roles = base.Set<Role>()); }
        }

        private IDbSet<Module> _modules;
        public IDbSet<Module> Modules
        {
            get { return _modules ?? (_modules = base.Set<Module>()); }
        }

        private IDbSet<Action> _actions;
        public IDbSet<Action> Actions
        {
            get { return _actions ?? (_actions = base.Set<Action>()); }
        }

        private IDbSet<Privilege> _privileges;
        public IDbSet<Privilege> Privileges
        {
            get { return _privileges ?? (_privileges = base.Set<Privilege>()); }
        }

        private IDbSet<Menu> _menus;
        public IDbSet<Menu> Menus
        {
            get { return _menus ?? (_menus = base.Set<Menu>()); }
        }

        #region DbContext Overrides

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Remove unused conventions
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Add entity configurations in a structured way using 'TypeConfiguration’ classes
            modelBuilder.Configurations.Add(new ModuleEntityConfiguration());
        }

        #endregion
    }
}
