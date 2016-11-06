using Ninject;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using Tache.Domain.Abstract;
using Tache.Domain.Concrete;
using Tache.Models;

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
            kernel.Bind<IActivityAndDurationsRepository>().To<ActivityAndDurationsRepository>();
            kernel.Bind<IActivityRepository>().To<EFActivityRepository>();
            kernel.Bind<IBudgetRepository>().To<EFBudgetRepository>();
            kernel.Bind<IDurationRepository>().To<EFDurationRepository>();
        }
    }
}