using System.Threading.Tasks;

namespace XFormsSkeleton.Framework
{
    public abstract class BaseViewModel : ExtendedBindableObject, IBaseViewModel
    {
    }

    public abstract class BaseViewModel<TNavData> : IBaseViewModel<TNavData>
    {
        public virtual Task InitAsync(TNavData navData)
        {
            return Task.FromResult(false);
        }
    }
}