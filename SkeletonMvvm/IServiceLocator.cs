using System;

namespace SkeletonMvvm
{
    public interface IServiceLocator
    {
        object Resolve(Type type);
    }

    public static class ServiceLocatorExtensions
    {
        public static T Resolve<T>(this IServiceLocator serviceLocator)
        {
            return (T) serviceLocator.Resolve(typeof(T));
        }
    }
}