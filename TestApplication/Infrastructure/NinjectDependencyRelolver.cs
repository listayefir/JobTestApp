using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using TestApplication.Abstract;
using TestApplication.Concrete;
namespace TestApplication.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParams)
        {
            kernel = kernelParams;
            AddBindings();
        }

        private void AddBindings()
        {
            kernel.Bind<ILannisterRepository>().To<EfLannisterRepository>();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}