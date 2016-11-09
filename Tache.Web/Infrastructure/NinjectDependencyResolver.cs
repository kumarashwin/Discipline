using Ninject;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using Tache.Domain.Abstract;
using Tache.Domain.Concrete;
using Ninject.Web.Common;
using Tache.Models.Abstract;
using Tache.Models.Concrete;

namespace Tache.Infrastructure {
    public class NinjectDependencyResolver : IDependencyResolver {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel) {
            this.kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType) => kernel.TryGet(serviceType);
        public IEnumerable<object> GetServices(Type serviceType) => kernel.GetAll(serviceType);

        private void AddBindings() {

            // Web
            kernel.Bind<IActivityViewModelRepository>().To<ActivityViewModelRepository>().InRequestScope();
            kernel.Bind<IDayViewModelRepository>().To<DayViewModelRepository>().InRequestScope();

            // Domain
            kernel.Bind<AbstractDbContext>().To<DbContext>().InRequestScope();
            kernel.Bind<IActivityRepository>().To<ActivityRepository>().InRequestScope();
            kernel.Bind<IBudgetRepository>().To<BudgetRepository>().InRequestScope();
            kernel.Bind<IDurationRepository>().To<DurationRepository>().InRequestScope();
        }
    }
}