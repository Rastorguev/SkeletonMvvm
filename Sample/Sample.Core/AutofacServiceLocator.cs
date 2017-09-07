using System;
using Autofac;
using Autofac.Features.ResolveAnything;
using SkeletonMvvm;

namespace Sample.Core
{
    public class AutofacServiceLocator : IServiceLocator
    {
        private static IContainer _container;

        public object Resolve(Type type)
        {
            var instance = _container.Resolve(type);

            return instance;
        }

        public void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();

            builder.RegisterType<PageResolver>().As<IPageResolver>().SingleInstance()
                .WithParameter("serviceLocator", this);

            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            _container?.Dispose();
            _container = builder.Build();
        }
    }
}