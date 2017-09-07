using Xamarin.Forms;
using XFormsSkeleton.Framework.ViewModels;

namespace XFormsSkeleton.Framework
{
    public interface IPageResolver
    {
        Page ResolvePage<TViewModel>() where TViewModel : IBaseViewModel;

        Page ResolvePage<TViewModel, TNavData>(TNavData navData)
            where TViewModel : IBaseViewModel<TNavData>;
    }
}