using Ninject;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using Tache.Domain.Abstract;
using Tache.Domain.Concrete;
using Tache.Models;
using Ninject.Web.Common;

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
            kernel.Bind<IActivityAndDurationsRepository>().To<ActivityAndDurationsRepository>().InRequestScope();
            kernel.Bind<IActivityRepository>().To<ActivityRepository>().InRequestScope();
            kernel.Bind<IBudgetRepository>().To<BudgetRepository>().InRequestScope();
            kernel.Bind<IDurationRepository>().To<DurationRepository>().InRequestScope();
        }
    }
}