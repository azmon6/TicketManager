using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Ninject.Web.Common;
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
            kernel.Bind<EntityContext>().ToSelf().InRequestScope();
            kernel.Bind<ITicketRepository>().To<EFTicketRepository>().InTransientScope();
            kernel.Bind<IUsersRepository>().To<EFUserRepository>().InTransientScope();
            kernel.Bind<ICartRepository>().To<EFCartRepository>().InTransientScope();
        }
    }
}