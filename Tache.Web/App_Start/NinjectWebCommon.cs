[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Tache.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Tache.App_Start.NinjectWebCommon), "Stop")]

namespace Tache.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using System.Collections.Generic;
    using Models.Abstract;
    using Models.Concrete;
    using Domain.Abstract;
    using Domain.Concrete;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                AddBindings(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(StandardKernel kernel) {
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.Web.WebApi.NinjectDependencyResolver(kernel);
            System.Web.Mvc.DependencyResolver.SetResolver(new CustomMvcDependencyResolver(kernel));
        }

        private static void AddBindings(StandardKernel kernel) {

            // Web
            kernel.Bind<IActivityViewModelRepository>().To<ActivityViewModelRepository>().InRequestScope();
            kernel.Bind<IDaysViewModelRepository>().To<DaysViewModelRepository>().InRequestScope();

            // Domain
            kernel.Bind<AbstractDbContext>().To<DbContext>().InRequestScope();
            kernel.Bind<IActivityRepository>().To<ActivityRepository>().InRequestScope();
            kernel.Bind<IBudgetRepository>().To<BudgetRepository>().InRequestScope();
            kernel.Bind<IDurationRepository>().To<DurationRepository>().InRequestScope();
            kernel.Bind<ICurrentActivityRepository>().To<CurrentActivityRepository>().InRequestScope();
        }
    }

    public class CustomMvcDependencyResolver : System.Web.Mvc.IDependencyResolver {
        private IKernel kernel;

        public CustomMvcDependencyResolver(IKernel kernel){
            this.kernel = kernel;
        }

        public object GetService(Type serviceType) => kernel.TryGet(serviceType);
        public IEnumerable<object> GetServices(Type serviceType) => kernel.GetAll(serviceType);
    }
}
