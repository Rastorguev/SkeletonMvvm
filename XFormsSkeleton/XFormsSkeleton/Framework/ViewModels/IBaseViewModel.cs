using System.Threading.Tasks;

namespace XFormsSkeleton.Framework.ViewModels
{
    public interface IBaseViewModel
    {
    }

    public interface IBaseViewModel<in TNavData> : IBaseViewModel
    {
        void Init(TNavData navData);
    }
}