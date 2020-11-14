using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using Ninject;
using TicketManager.Domain.Abstract;
using TicketManager.Domain.Concrete;

namespace TicketManager.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<EntityContext>().ToSelf().InSingletonScope();
            kernel.Bind<ITicketRepository>().To<EFTicketRepository>();
            kernel.Bind<IUsersRepository>().To<EFUserRepository>();
        }
    }
}