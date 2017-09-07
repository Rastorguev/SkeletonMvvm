namespace SkeletonMvvm
{
    public abstract class BaseViewModel : ExtendedBindableObject, IBaseViewModel
    {
    }

    public abstract class BaseViewModel<TNavData> : IBaseViewModel<TNavData>
    {
        public virtual void Init(TNavData navData)
        {
        }
    }
}