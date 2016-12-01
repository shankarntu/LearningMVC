using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using System.Web.Mvc;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Concrete;


namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public Object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IPorductsRepository>().To<EFProductRepository>();
        }
        private void AddBindings_Mock()
        {
            Mock<IPorductsRepository> mock = new Mock<IPorductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { Name="Football", Price=25},
                new Product { Name="Surf Board", Price=179},
                new Product { Name="Running shoes", Price=95}
            });
            kernel.Bind<IPorductsRepository>().ToConstant(mock.Object);
        }
    }
}