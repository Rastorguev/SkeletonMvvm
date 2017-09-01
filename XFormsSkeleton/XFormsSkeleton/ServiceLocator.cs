using System.Reflection;
using Autofac;
using Autofac.Features.ResolveAnything;
using XFormsSkeleton.ViewModels;

namespace XFormsSkeleton
{
    public static class ServiceLocator
    {
        private static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            _container?.Dispose();
            _container = builder.Build();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}