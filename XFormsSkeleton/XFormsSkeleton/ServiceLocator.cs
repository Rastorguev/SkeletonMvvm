using Autofac;
using Autofac.Features.ResolveAnything;

namespace XFormsSkeleton
{
    public interface IServiceLocator
    {
        T Resolve<T>();
    }

    public class ServiceLocator : IServiceLocator
    {
        private static IContainer _container;

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance()
                .WithParameter("serviceLocator", this);
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            _container?.Dispose();
            _container = builder.Build();
        }
    }
}