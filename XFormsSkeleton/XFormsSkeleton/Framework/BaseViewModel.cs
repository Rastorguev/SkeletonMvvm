using System.Threading.Tasks;

namespace XFormsSkeleton.Framework
{
    public abstract class BaseViewModel<TNavData> : ExtendedBindableObject
    {
        public virtual Task InitAsync(TNavData navData)
        {
            return Task.FromResult(false);
        }
    }

    public abstract class BaseViewModel : BaseViewModel<object>
    {
    }
}