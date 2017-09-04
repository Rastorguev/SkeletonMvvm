using System.Threading.Tasks;

namespace XFormsSkeleton.Framework
{
    public interface IBaseViewModel
    {
    }

    public interface IBaseViewModel<in TNavData>
    {
        Task InitAsync(TNavData navData);
    }
}