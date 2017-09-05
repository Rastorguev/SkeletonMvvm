using System.Threading.Tasks;

namespace XFormsSkeleton.Framework
{
    public interface IBaseViewModel
    {
    }

    public interface IBaseViewModel<in TNavData> : IBaseViewModel
    {
        Task InitAsync(TNavData navData);
    }
}