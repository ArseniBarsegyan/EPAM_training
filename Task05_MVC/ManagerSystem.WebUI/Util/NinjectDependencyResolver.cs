using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ManagerSystem.BLL.Interfaces;
using ManagerSystem.BLL.Services;
using ManagerSystem.DAL.Interfaces;
using ManagerSystem.DAL.Repositories;
using Ninject;

namespace ManagerSystem.WebUI.Util
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
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(ConstantStorage.ConnectionString);

            kernel.Bind<IUnitOfWork>()
                .To<UnitOfWork>()
                .WhenInjectedInto<OrderService>()
                .WithConstructorArgument(ConstantStorage.ConnectionString);

            kernel.Bind<IUnitOfWork>()
                .To<UnitOfWork>()
                .WhenInjectedInto<ProductService>()
                .WithConstructorArgument(ConstantStorage.ConnectionString);

            kernel.Bind<IUnitOfWork>()
                .To<UnitOfWork>()
                .WhenInjectedInto<ManagerService>()
                .WithConstructorArgument(ConstantStorage.ConnectionString);

            kernel.Bind<IOrderService>().To<OrderService>();
            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<IManagerService>().To<ManagerService>();
        }
    }
}