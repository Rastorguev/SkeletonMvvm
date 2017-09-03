namespace XFormsSkeleton.Framework
{
    public interface IServiceLocator
    {
        T Resolve<T>();
    }
}