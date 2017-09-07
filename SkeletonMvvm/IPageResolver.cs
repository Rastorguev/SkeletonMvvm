using Xamarin.Forms;

namespace SkeletonMvvm
{
    public interface IPageResolver
    {
        Page ResolvePage<TViewModel>() where TViewModel : IBaseViewModel;

        Page ResolvePage<TViewModel, TNavData>(TNavData navData)
            where TViewModel : IBaseViewModel<TNavData>;
    }
}